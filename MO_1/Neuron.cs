
namespace MO_1
{
    /// <summary>
    /// Один единственный персептрон, расчитанный на работу с 2д входными данными.
    /// </summary>
    class Neuron
    {
        /// <summary>
        /// Результат работы нейрона.
        /// </summary>
        private float sum;

        /// <summary>
        /// Входные значения нейрона.
        /// </summary>
        private int[,] input;

        /// <summary>
        /// Веса нейрона.
        /// </summary>
        private float[,] weights;

        /// <summary>
        /// Промежуточные значения нейрона.
        /// </summary>
        private float[,] values;

        /// <summary>
        /// Пороговое значение для активации нейрона.
        /// </summary>
        private float porog;

        /// <summary>
        /// Количество входов.
        /// </summary>
        private int sizeX, sizeY;

        /// <summary>
        /// Возвращает массив весов.
        /// </summary>
        /// <returns>Массив весов.</returns>
        public float[,] GetWeights()
        {
            return this.weights;
        }

        /// <summary>
        /// Определяет веса нейрона на основе информации из файла
        /// </summary>
        /// <param name="line">строка текста из файла</param>
        public void SetWeights(string line)
        {
            for(int i = 0; i < this.sizeX; i++)
                for (int j = 0; j < this.sizeY; j++)
                {
                    string s = line.Substring(0, line.IndexOf(" "));
                    line = line.Remove(0, line.IndexOf(" ") + 1);
                    this.weights[i, j] = float.Parse(s);
                }
        }

        /// <summary>
        /// Конструктор по умолчанию. Задаёт количество входов в нейрон и пороговое значение.
        /// </summary>
        /// <param name="sizeX">Количество входов по оси 0X</param>
        /// <param name="sizeY">Количество входов по сои 0Y</param>
        /// <param name="porog">Пороговое значение для нейрона</param>
        public Neuron(int sizeX, int sizeY, float porog)
        {
            this.sum = 0;
            this.input = new int[sizeX, sizeY];
            this.weights = new float[sizeX, sizeY];
            this.porog = porog;
            this.values = new float[sizeX, sizeY];
            this.sizeX = sizeX;
            this.sizeY = sizeY;
        }

        /// <summary>
        /// Перемножение входов на вес этих входов.
        /// </summary>
        public void MulInps()
        {
            for (int i = 0; i < this.sizeX; i++)
                for (int j = 0; j < this.sizeY; j++)
                    this.values[i, j] = this.weights[i, j] * this.input[i, j];
        }


        /// <summary>
        /// Задаёт значение на определённом синапсе нейрона.
        /// </summary>
        /// <param name="value">Принимаемое значение</param>
        /// <param name="X">Инднекс синапса по оси 0X</param>
        /// <param name="Y">Индекс синапса по оси 0Y</param>
        public void SetInputAtXY(int value, int X, int Y)
        {
            if (X < this.sizeX && Y < this.sizeY)
                this.input[X, Y] = value;
        }

        /// <summary>
        /// Производит суммирование всех значений выводов нейрона.
        /// </summary>
        /// <returns>Возвращает истину, если нейрон преодолел порог, иначе ложь.</returns>
        public bool Sum()
        {
            for (int i = 0; i < this.sizeX; i++)
                for (int j = 0; j < this.sizeY; j++)
                    this.sum += this.values[i, j];
            return this.sum > this.porog;
        }


        /// <summary>
        /// Отчищает входы нейрона от информации.
        /// </summary>
        public void Reset()
        {
            this.sum = 0;
            for (int i = 0; i < this.sizeX; i++)
                for (int j = 0; j < this.sizeY; j++)
                    this.input[i, j] = 0;
        }

        ///<summary>
        ///Если её неправильный ответ False, то прибавляем значения входов к весам каждой ножки (к ножке 1 — значение в точке [0,0] картинки и т.д.)
        ///</summary>
        public void incW()
        {
            for (int x = 0; x < this.sizeX; x++)
            {
                for (int y = 0; y < this.sizeY; y++)
                {
                    this.weights[x, y] += this.input[x, y];
                }
            }
        }

        ///<summary>
        ///Если её неправильный ответ True, то вычитаем значения входов из веса каждой ножки
        ///</summary>
        public void decW()
        {
            for (int x = 0; x < this.sizeX; x++)
            {
                for (int y = 0; y < this.sizeY; y++)
                {
                    this.weights[x, y] -= this.input[x, y];
                }
            }
        }

        /// <summary>
        /// Функция тренировки сети согласно Дельта-парвилу
        /// </summary>
        /// <param name="delta">соотношение результата и ожидания</param>
        public void DeltaTrain(int delta)
        {
            for (int i = 0; i < this.sizeX; i++)
                for (int j = 0; j < this.sizeY; j++)
                    this.weights[i, j] += delta * (float)0.1 * this.input[i, j];
        }
    }
}
