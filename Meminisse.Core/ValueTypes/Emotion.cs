namespace Meminisse.Core.ValueTypes;

[Flags]
public enum Emotion
{
    Neutral = 0,
    Joy = 1 << 0,
    Sadness = 1 << 1,
    Anger = 1 << 2,
    Love = 1 << 3,
    Fear = 1 << 4,
    Surprise = 1 << 5,
    Hope = 1 << 6,
    Disgust = 1 << 7,
    Pride = 1 << 8,
    Gratitude = 1 << 9,
    Loneliness = 1 << 10,
    Excitement = 1 << 11,
    Nostalgia = 1 << 12
}