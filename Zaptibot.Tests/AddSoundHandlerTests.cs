using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NSubstitute;
using Zaptibot.Api.Settings;
using Zaptibot.Api.Sounds;
using Zaptibot.Api.Sounds.Handlers;
using Zaptibot.Api.Sounds.Models.Entity;
using Zaptibot.Api.Sounds.Repository;

namespace Zaptibot.Tests;

public class AddSoundHandlerTests
{
    private readonly string _soundsPath;
    private readonly string _soundValueSettings = "SoundSettings:SoundsPath";
    
    public AddSoundHandlerTests()
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
        string name = "TestSound";
        ISoundRepository? mockSoundRepository = Substitute.For<ISoundRepository>();
        AddSoundHandler addSoundHandler = new AddSoundHandler(mockSoundRepository, Options.Create(new SoundSettings { Path = _soundsPath }));

        mockSoundRepository.AddAsync(Arg.Any<Sound>()).Returns(callInfo => callInfo.Arg<Sound>());

        // Act
        Sound result = await addSoundHandler.HandleAsync(name);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(name);
        await mockSoundRepository.Received(1).AddAsync(Arg.Is<Sound>(sound => sound.Name == name));
    }
}