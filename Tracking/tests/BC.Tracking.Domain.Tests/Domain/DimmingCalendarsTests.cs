using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Connexity.BC.Tracking.Domain.Tests.Domain
{
    using Connexity.BC.Tracking.Domain.Entities;
    using Connexity.BC.Tracking.Domain.Events;
    using Connexity.BC.Tracking.Domain.Ports;
    using Connexity.BC.Tracking.Domain.Requests;
    using Connexity.BC.Tracking.Domain.States;
    using Connexity.BC.Tracking.Domain.Tests.Helpers;
    using Connexity.BC.Tracking.Domain.ValueObject;
    using Connexity.BC.Tracking.Service.Adapters.Api.Requests;
    using static Connexity.BC.Tracking.Domain.Requests.Models.Request;
    
    
    using Connexity.BC.Tracking.Domain.Events.Models;
    
    using static Connexity.BC.Tracking.Domain.States.TrackingState;
    using Connexity.BC.Tracking.Domain.Interfaces;
    using static Connexity.BC.Tracking.Domain.Entities.Tracking;

    public class TrackingTests
    {
        /// <summary>
        /// The store port
        /// </summary>
        private readonly IStorePort _storePort;

        /// <summary>
        /// The notify port
        /// </summary>
        private readonly Mock<INotifyPort> _notifyPort;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The validator factory
        /// </summary>
        private readonly IValidatorFactory _validatorFactory;

        /// <summary>
        /// The fixture
        /// </summary>
        private readonly IFixture _fixture;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<Tracking> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackingTests"/> class.
        /// </summary>
        public TrackingTests(ITestOutputHelper output)
        {
            this._fixture = new Fixture().Customize(new AutoMoqCustomization());
            this._storePort = InitHelper.CreateStorePort();
            this._notifyPort = InitHelper.CreateNotifyPort();
            this._mapper = InitHelper.CreateMapper();
            this._validatorFactory = InitHelper.CreateValidatorFactory();
#if DEBUG        
            this._logger = new LoggedLogger<Tracking>(output);
#else            
            this._logger = Mock.Of<ILogger<Tracking>>();
#endif
        }

        /// <summary>
        /// Get request should get all the Trackings.
        /// </summary>
        [Fact]
        public async Task DetailRequest_ShouldGetOneTracking()
        {
            int nbOfTrackingStateToCreate = 99;

            TrackingState[] TrackingStates = this._fixture
                .Build<TrackingState>()
                .CreateMany(nbOfTrackingStateToCreate)
                .ToArray();

            var getOneStorePort = InitHelper.CreateStorePort(TrackingStates);
            var Tracking = new Tracking(getOneStorePort, _notifyPort.Object, _mapper, _validatorFactory,  this._logger);
            Tracking tracking = await Tracking.Handle(new DetailRequest { TrackingId = TrackingStates[22].Id }, new CancellationToken());

            Tracking.Id.Should().Be(TrackingStates[22].Id);
            Tracking.Label.Should().Be(TrackingStates[22].Label);
        }

        /// <summary>
        /// Detail request should throw an error if id is not found
        /// </summary>
        [Fact]
        public async Task DetailRequestWithWrongId_ShouldThrowAnException()
        {
            int nbOfTrackingStateToCreate = 99;

            TrackingState[] TrackingStates = this._fixture
                .Build<TrackingState>()
                .CreateMany(nbOfTrackingStateToCreate).ToArray();

            var getOneStorePort = InitHelper.CreateStorePort(TrackingStates);
            var Tracking = new Tracking(getOneStorePort, _notifyPort.Object, _mapper, _validatorFactory,  this._logger);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => Tracking.Handle(new DetailRequest { TrackingId = Guid.NewGuid() }, new CancellationToken()));
        }

        /// <summary>
        /// InitCreate request without errors should execute INotifyPort.NotifyAsync to publish an Energy-TrackingChangeRequested event.
        /// </summary>
        [Fact]
        public async Task InitCreateRequestWithoutErrors_ShouldPublishAnEvent()
        {
            _notifyPort.Setup(x => x.NotifyAsync(It.IsAny<EnergyTrackingChangeRequested>(), It.IsAny<CancellationToken>())).Verifiable();

            Tracking Tracking = new(_storePort, _notifyPort.Object, _mapper, _validatorFactory,  this._logger);
            InitCreateRequest initCreateRequest = this._fixture.Build<InitCreateRequest>()
                .With(x => x.ValidationErrors, new List<ValidationError>())
                .Create();

            CancellationToken cancellationToken = CancellationToken.None;
            await Tracking.Handle(initCreateRequest, cancellationToken);
            _notifyPort.Verify();
        }

        /// <summary>
        /// InitCreate request with errors should execute INotifyPort.NotifyAsync to publish an Energy-TrackingChangeRequested event.
        /// </summary>
        [Fact]
        public async Task InitCreateRequestWithErrors_ShouldPublishAnEvent()
        {
            _notifyPort.Setup(x => x.NotifyAsync(It.IsAny<EnergyTrackingChangeRequested>(), It.IsAny<CancellationToken>())).Verifiable();

            Tracking Tracking = new(_storePort, _notifyPort.Object, _mapper, _validatorFactory,  this._logger);
            InitCreateRequest initCreateRequest = this._fixture.Build<InitCreateRequest>()
                .With(x => x.ValidationErrors, new List<ValidationError>() { new ValidationError { AppId = "test", Timestamp = DateTimeOffset.UtcNow, Code = 400, Message = "Test error" } })
                .Create();

            CancellationToken cancellationToken = CancellationToken.None;
            await Tracking.Handle(initCreateRequest, cancellationToken);
            _notifyPort.Verify();
        }

        /// <summary>
        /// Create request without errors should create new Tracking.
        /// </summary>
        [Fact]
        public async Task CreateRequestWithoutErrors_ShouldCreateNewTracking()
        {
            var Tracking = new Tracking(_storePort, _notifyPort.Object, _mapper, _validatorFactory,  this._logger);

            List<DimmingProgramWeekDayRequest> dimmingProgramWeekDayRequests = Enum.GetValues<DayOfWeek>()
                .Select(dow => new DimmingProgramWeekDayRequest()
                { 
                    DayOfWeek = dow, DimmingProgramId = Guid.NewGuid()
                }).ToList();

            CreateRequest createRequest = this._fixture.Build<CreateRequest>()
                .With(x => x.ValidationErrors, new List<ValidationError>())
                .Create();

            await Tracking.Handle(createRequest, new CancellationToken());

            Tracking TrackingFound = await Tracking.Handle(new DetailRequest { TrackingId = createRequest.Id }, new CancellationToken());

            TrackingFound.Id.Should().Be(createRequest.Id);
            TrackingFound.Code.Should().Be(createRequest.Code);
        }

        /// <summary>
        /// Create request with errors should not create new Tracking.
        /// </summary>
        [Fact]
        public async Task CreateRequestWithErrors_ShouldNotCreateNewTracking()
        {
            var Tracking = new Tracking(_storePort, _notifyPort.Object, _mapper, _validatorFactory,  this._logger);
            CreateRequest createRequest = this._fixture.Build<CreateRequest>()
                .With(x => x.ValidationErrors, new List<ValidationError>() { new ValidationError { AppId = "test", Timestamp = DateTimeOffset.UtcNow, Code = 400, Message = "Test error" } })
                .Create();
            await Tracking.Handle(createRequest, new CancellationToken());

            await Assert.ThrowsAsync<KeyNotFoundException>(() => Tracking.Handle(new DetailRequest { TrackingId = createRequest.Id }, new CancellationToken()));
        }

        /// <summary>
        /// Create request without errors should execute INotifyPort.NotifyAsync to publish an Energy-TrackingChanged event.
        /// </summary>
        [Fact]
        public async Task CreateRequestWithoutErrors_ShouldPublishACreatedEventAndAHistorizeEvent()
        {
            _notifyPort.Setup(x => x.NotifyAsync(It.IsAny<EnergyTrackingChanged<DimmingProgramWeekDayEntity, DimmingProgramExceptionDayEntity>>(), It.IsAny<CancellationToken>())).Verifiable();
            Tracking Tracking = new(_storePort, _notifyPort.Object, _mapper, _validatorFactory,  this._logger);

            List<DimmingProgramWeekDayRequest> dimmingProgramWeekDayRequests = Enum.GetValues<DayOfWeek>()
                .Select(dow => new DimmingProgramWeekDayRequest()
                {
                    DayOfWeek = dow,
                    DimmingProgramId = Guid.NewGuid()
                }).ToList();

            CreateRequest createRequest = this._fixture.Build<CreateRequest>()
                .With(x => x.ValidationErrors, new List<ValidationError>())
                .Create();

            CancellationToken cancellationToken = CancellationToken.None;
            await Tracking.Handle(createRequest, cancellationToken);
            _notifyPort.Verify();
        }

        /// <summary>
        /// Create request without errors should execute INotifyPort.NotifyAsync to publish an Energy-TrackingChanged event.
        /// </summary>
        [Fact]
        public async Task CreateRequestWithErrors_ShouldPublishANotCreatedEventAndNotAnHistorizeEvent()
        {
            _notifyPort.Setup(x => x.NotifyAsync(It.IsAny<EnergyTrackingChanged<DimmingProgramWeekDayRequest, DimmingProgramExceptionDayRequest>>(), It.IsAny<CancellationToken>())).Verifiable();

            Tracking Tracking = new(_storePort, _notifyPort.Object, _mapper, _validatorFactory,  this._logger);
            CreateRequest createRequest = this._fixture.Build<CreateRequest>()
                .With(x => x.ValidationErrors, new List<ValidationError>() { new ValidationError { AppId = "test", Timestamp = DateTimeOffset.UtcNow, Code = 400, Message = "Test error" } })
                .Create();

            CancellationToken cancellationToken = CancellationToken.None;
            await Tracking.Handle(createRequest, cancellationToken);
            _notifyPort.Verify();
        }

        /// <summary>
        /// Should historize the creation of new Tracking.
        /// </summary>
        [Fact(DisplayName = nameof(Should_call_historize_method))]
        public async Task Should_call_historize_method()
        {
            var Tracking = new Tracking(_storePort, _notifyPort.Object, _mapper, _validatorFactory,  this._logger);
            var createRequest = new CreateRequest()
            {
                UserId = "TestUserId",
                Id = Guid.NewGuid(),
                Description = "Test régime",
                Code = "test",
                Label = "1234",
                Mode = OperatingMode.MidnightMidnight,
                ValidationErrors = new List<ValidationError>(),
            };
            await Tracking.Handle(createRequest, new CancellationToken());
        }

     
        /// <summary>
        /// test the conversion from json files to WebRequest poco object then to CreateRequest poco object
        /// </summary>
        [Fact(DisplayName = nameof(FilesJsonContent_MapAndValidate))]
        public void FilesJsonContent_MapAndValidate()
        {
            bool testSuccess = true;
            string jsonDir = Path.Combine(Environment.CurrentDirectory, "Json");
            Assert.True(Directory.Exists(jsonDir), "Environment.CurrentDirectory NOT FOUND : " + jsonDir);
            foreach (var file in Directory.GetFiles(jsonDir, "*.json"))
            {
                var filenameParts = new FileInfo(file).Name.Split(new[] { '_', '.' });
                if (filenameParts.Length != 4) throw new FormatException($"File '{file}' has wrong format : should be TWebRequest_TReq_[OK|KO].json");

                var jsonType = InitHelper.FindType(filenameParts[0]);
                Assert.NotNull(jsonType);
                var destType = InitHelper.FindType(filenameParts[1]);
                Assert.NotNull(destType);

                _logger.LogDebug("Treating file {@file}", file);

                if (jsonType.Equals(typeof(CreateTrackingWebRequest)))
                {
                    testSuccess = TreatFile<CreateTrackingWebRequest, InitCreateRequest>(testSuccess, file, filenameParts[2] /* OK or KO */);
                }
            }
            Assert.True(testSuccess, "Errors found while testing Json files!");
        }

        private bool TreatFile<TWebRequest, TReq>(bool testSuccess, string file, string shouldBe)
        {
            string jsonContent = File.ReadAllText(file);
            List<TWebRequest> jsonObjects = JsonConvert.DeserializeObject<List<TWebRequest>>(jsonContent);
            jsonObjects.ForEach(jsonObject =>
            {
                testSuccess &= MapAndValidate<TWebRequest, TReq>(jsonObject, shouldBe);
            });
            return testSuccess;
        }

        private bool MapAndValidate<TWebRequest, TReq>(TWebRequest webRequestObject, string shouldBe)
        {
            TReq mappedObject = _mapper.Map<TWebRequest, TReq>(webRequestObject);
            IValidator<TReq> validator = _validatorFactory.GetValidator<TReq>();
            ValidationResult result = validator.Validate(mappedObject);
            bool expectedResult = shouldBe == "OK";
            bool testSuccess = expectedResult == result.IsValid;
            string logMessage = $"Test payload with code : {(webRequestObject as dynamic).Code} {(testSuccess ? "SUCCESS" : "FAILED")} : {typeof(TReq).Name} IsValid {result.IsValid}, expected {expectedResult} with {result.Errors.Count} errors";

            Action<ILogger, string> actionLog = null;
            if (!testSuccess)
            {
                actionLog = (logger, msg) => logger.LogError("{@msg}", msg);
            }
            else
            {
                actionLog = (logger, msg) => logger.LogInformation("{@msg}", msg);
            }

            actionLog(_logger, logMessage);
            if (!result.IsValid)
            {
                result.Errors.ForEach(failure => actionLog(_logger, failure.ErrorMessage));
            }
            return testSuccess;
        }

        private static List<TDimmingProgram> GetDimmingProgramExceptionDayList<TDimmingProgram>()
            where TDimmingProgram : IDimmingProgramExceptionDay, new()
        {
            return new List<TDimmingProgram>()
                {
                    new TDimmingProgram()
                    {
                        DimmingProgramId = Guid.NewGuid(),
                        StartDate = new DateOnly(2021,12,12),
                        EndDate = new DateOnly(2021,12,12),
                        ExceptionType = ExceptionType.Fixed,
                    }
                };
        }
    }
}