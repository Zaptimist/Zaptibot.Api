using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Zaptibot.Api.Sounds;
using Zaptibot.Api.Sounds.Repository;

namespace Zaptibot.Tests;

public class RegisterSoundTests
{
    private readonly string _soundsPath;
    private readonly string _soundValueSettings = "SoundSettings:SoundsPath";
    
    public RegisterSoundTests()
    {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
        
        IConfiguration configuration = configurationBuilder.Build();
        _soundsPath = configuration.GetValue<string>(_soundValueSettings) 
              ?? throw new InvalidOperationException($"{_soundValueSettings} is not set in the appsettings.Development.json file.");
    }
    
    [Fact]
    public async Task Handle_ShouldAddSound_ReturnSound()
    {
        // Arrange
        ISoundRepository? mockSoundRepository = Substitute.For<ISoundRepository>();
        RegisterSound registerSound = new (mockSoundRepository);
        RegisterSound.Request request = new ("TestSound", _soundsPath);

        mockSoundRepository.AddAsync(Arg.Any<Sound>()).Returns(callInfo => callInfo.Arg<Sound>());

        // Act
        Sound result = await registerSound.Handle(request);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Path.Should().Be(request.Path);
        await mockSoundRepository.Received(1).AddAsync(Arg.Is<Sound>(s => s.Name == request.Name && s.Path == request.Path));
    }
}