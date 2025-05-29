using System;

[Serializable]
public class QuizQuestion
{
    public string questionText;
    public string[] answers = new string[4];
    public int correctAnswerIndex; // Правильный ответ (0–3)
}
