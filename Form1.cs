using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Homework_25_08
{
    public partial class Form1 : Form
    {
        public enum Question_type
        {
            single_answer,
            multiple_answers,
            manual_input
        }

        public class Question
        {
            public string Question_text { get; set; }
            public List<string> answers = new List<string>();
            public List<string> right = new List<string>();
            public Question_type question_type { get; set; }

            public Question(string question_text, List<string> answers, List<string> right, Question_type question_type)
            {
                Question_text = question_text;
                this.answers = answers;
                this.right = right;
                this.question_type = question_type;
            }
        }

        private List<Question> question_list = new List<Question>();
        private int question_index = 0;
        private int score = 0;

        private void InitializeQuestions()
        {
            question_list = new List<Question>
            {
                new Question("1. Выберите органы, вывод из строя которых может привести\n    к мгновенному летальному исходу.", new List<string> { "Мозг", "Легкие", "Сердце", "Желудок", "Печень", "Почки"}, new List<string> { "Мозг", "Сердце", "Легкие" }, Question_type.multiple_answers),

                new Question("2. Является ли зуб органом?", new List<string> {"Да","Нет"}, new List<string> {"Да"}, Question_type.single_answer),

                new Question("3. Сколько групп крови без учета резус-фактора имеет человек?", null, new List<string> {"Четыре"}, Question_type.manual_input),

                new Question("4. Сколько групп крови с учетом резус-фактора имеет человек?", null, new List<string> {"Восемь"}, Question_type.manual_input),

                new Question("5. Известно что кровеносные сосуды делятся на\n    капеляры, вены и артерии. Какой из них самый крупный?", new List<string> {"Капеляр", "Вена", "Артерия" }, new List<string> {"Артерия"}, Question_type.single_answer),

                new Question("6. Известно что кровеносные сосуды делятся на\n    капеляры, вены и артерии. Какой из них самый мелкий?", new List<string> {"Капеляр", "Вена", "Артерия" }, new List<string> {"Капеляр"}, Question_type.single_answer),

                new Question("7. Сколько слоёв кожи имеет человек?", null, new List<string> {"Три"}, Question_type.manual_input),

                new Question("8. Из вариантов ниже выберите название внешнего слоя кожи.", new List<string> {"Гиподерма", "Гипердерма", "Эпидерма", "Дерма", "Эпидермис", "Дермис" }, new List<string> {"Эпидермис"}, Question_type.single_answer),

                new Question("9. Как расшифровывается абревиатура АД?", new List<string> {"Анатомическое давление","Анальное давление", "Артериальное давление" }, new List<string> {"Артериальное давление"}, Question_type.single_answer),

                new Question("10. Из вариантов ниже выберите термин который означает повышенное АД.", new List<string> {"Гомотония", "Гипертония", "Гипотония" }, new List<string> {"Гипертония"}, Question_type.single_answer),

                new Question("11. Из вариантов ниже выберите термин который означает пониженное АД.", new List<string> {"Гомотония", "Гипертония", "Гипотония" }, new List<string> {"Гипотония"}, Question_type.single_answer),

                new Question("12. Повреждением слоёв кожи в следствии воздействия на неё\n    высоких температур химикатов, электрического тока или\n    радиационного излучения называют:", null, new List<string> {"Ожог"}, Question_type.manual_input),

                new Question("13. Из вариантов ниже выберите вид ожога который возникает\n    в следствии воздействия на кожу радиационного излучения.", new List<string> {"Световой", "Химический", "Термический", "Кислотный", "Электрический", "Лучевой" }, new List<string> {"Лучевой"}, Question_type.single_answer),

                new Question("14. Сколько существует степеней ожога?", null, new List<string> {"Четыре"}, Question_type.manual_input),

                new Question("15. Отросток на конце слепой кишки воспаление которого называют Аппендицитом.", null, new List<string> {"Аппендикс"}, Question_type.manual_input)
            };
        }

        private void ShowQuestion(int index)
        {
            if (index < question_list.Count)
            {
                Question question = question_list[index];
                label1.Text = question.Question_text;

                label2.Visible = false;
                label3.Visible = false;
                textBox1.Visible = false;
                for (int i = 0; i < 6; i++)
                {
                    Controls.Find("checkBox" + (i + 1), true)[0].Visible = false;
                }

                if (question.question_type == Question_type.single_answer || question.question_type == Question_type.multiple_answers)
                {
                    for (int i = 0; i < question.answers.Count; i++)
                    {
                        CheckBox checkBox = (CheckBox)Controls.Find("checkBox" + (i + 1), true)[0];
                        checkBox.Text = question.answers[i];
                        checkBox.Visible = true;

                        if (question.question_type == Question_type.single_answer)
                        {
                            checkBox.CheckedChanged += SingleAnswerCheckbox_CheckedChanged;
                        }
                    }
                }
                else if (question.question_type == Question_type.manual_input)
                {
                    textBox1.Text = "";
                    textBox1.Visible = true;
                }
            }
            else
            {
                label1.Visible = false;
                textBox1.Visible = false;
                button1.Visible = false;
                label2.Visible = true;
                label3.Visible = true;
                label2.Text = "Статистика:";
                label3.Text = $"Количество балов: {score} из {question_list.Count}";
            }

            foreach (CheckBox checkBox in Controls.OfType<CheckBox>())
            {
                checkBox.Checked = false;
            }
        }

        private void SingleAnswerCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
            {
                foreach (CheckBox otherCheckBox in Controls.OfType<CheckBox>())
                {
                    if (otherCheckBox != checkBox)
                    {
                        otherCheckBox.Checked = false;
                    }
                }
            }
        }

        private bool CheckAnswer(List<CheckBox> checkBoxes, List<string> rightAnswers)
        {
            List<string> selectedAnswers = new List<string>();
            foreach (CheckBox checkBox in checkBoxes)
            {
                if (checkBox.Checked)
                {
                    selectedAnswers.Add(checkBox.Text);
                }
            }
            return selectedAnswers.Count == rightAnswers.Count && selectedAnswers.All(answer => rightAnswers.Contains(answer));
        }


        public Form1()
        {
            InitializeComponent();
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            textBox1.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
            checkBox5.Visible = false;
            checkBox6.Visible = false;
            button1.Visible = false;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = "Правила теста:\n- тест состоит из 15 вопросов\n- за каждый правильный ответ на вопрос будет нащитыватся 1 балл\n- баллы не будут защитыватся если ответа нет или он неверный\n- ответ на вопрос который требует ввод вручную должен быть буквенным\n- при этом такой ответ должен начинаться с заглавной буквы\n-точки в конце таких ответов ставить не нужно";
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (question_index < question_list.Count)
            {
                List<CheckBox> checkBoxes = new List<CheckBox> { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6 };
                Question currentQuestion = question_list[question_index];

                if (currentQuestion.question_type == Question_type.single_answer || currentQuestion.question_type == Question_type.multiple_answers)
                {
                    if (CheckAnswer(checkBoxes, currentQuestion.right))
                    {
                        score++;
                    }
                }
                else if (currentQuestion.question_type == Question_type.manual_input)
                {
                    string userInput = textBox1.Text.Trim();

                    if (currentQuestion.right.Contains(userInput))
                    {
                        score++;
                    }
                }

                question_index++;
                ShowQuestion(question_index);
            }
            else
            {
                label1.Visible = false;
                textBox1.Visible = false;
                label2.Visible = true;
                label3.Visible = true;
                label2.Text = "Статистика:";
                label3.Text = $"Количество балов: {score} из {question_list.Count}";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            label4.Visible = false;

            label1.Visible = true;
            button1.Visible = true;

            InitializeQuestions();
            ShowQuestion(question_index);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
