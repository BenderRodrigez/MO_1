
namespace MO_1
{
    /// <summary>
    /// Сеть из N простых персептронов.
    /// </summary>
    class NeuronNet
    {
        /// <summary>
        /// Сеть из нейронов.
        /// </summary>
        private Neuron[] neurons;

        /// <summary>
        /// Количество нейронов.
        /// </summary>
        private int size;

        /// <summary>
        ///  Количетво входов.
        /// </summary>
        private int sizeX, sizeY;

        /// <summary>
        /// Результат работы нейронной сети в виде массива.
        /// </summary>
        private bool[] results;

        /// <summary>
        /// Конструктор, при чтении из файла
        /// </summary>
        /// <param name="lines">текст с информацией о весах и тп</param>
        /// <param name="porog">пороговое значение сети</param>
        public NeuronNet(string[] lines, float porog)
        {
            this.size = int.Parse(lines[0]);
            this.sizeX = int.Parse(lines[1]);
            this.sizeY = int.Parse(lines[2]);
            this.neurons = new Neuron[this.size];
            this.results = new bool[this.size];
            for (int i = 3; i < lines.Length; i++)
            {
                this.neurons[i - 3] = new Neuron(this.sizeX, this.sizeY, porog);
                this.neurons[i - 3].SetWeights(lines[i]);
            }
        }

        /// <summary>
        /// Подготавливает настройки сети для записи в файл
        /// </summary>
        /// <returns>Возвращает строковое представление информации хранимой в сети</returns>
        public string[] SetReadyToWrite()
        {
            string[] lines = new string[3+this.neurons.Length];
            lines[0] = this.size.ToString();
            lines[1] = this.sizeX.ToString();
            lines[2] = this.sizeY.ToString();
            for (int i = 3; i < lines.Length; i++)
            {
                lines[i] = "";
                float[,] tmp = this.neurons[i - 3].GetWeights();
                for (int j = 0; j < this.sizeX; j++)
                    for (int k = 0; k < this.sizeY; k++)
                        lines[i] += tmp[j, k].ToString() + " ";
            }
            return lines;
        }


        /// <summary>
        /// Конструктор, вызывается при первом запуске программы, когда нет файла с конфигурацией сети
        /// </summary>
        /// <param name="vol_symb">количество символов (и персептронов) с которыми сеть будет работать</param>
        /// <param name="sizeX">Размер изображения по Х</param>
        /// <param name="sizeY">Размер изображения по Y</param>
        /// <param name="porog">Пороговое значение для определения результата</param>
        public NeuronNet(int vol_symb, int sizeX, int sizeY, float porog)
        {
            this.size = vol_symb;
            this.neurons = new Neuron[this.size];
            this.results = new bool[this.size];
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            for (int k = 0; k < this.size; k++)
                this.neurons[k] = new Neuron(this.sizeX, this.sizeY, porog);
        }

        /// <summary>
        /// Вводт информацию о изображении в сеть
        /// </summary>
        /// <param name="image">Информация о изображении</param>
        public void SetInputData(int[,] image)
        {
            for (int k = 0; k < this.size; k++)
                for (int i = 0; i < this.sizeX; i++)
                    for (int j = 0; j < this.sizeY; j++)
                        this.neurons[k].SetInputAtXY(image[i, j], i, j);
        }

        /// <summary>
        /// Подсчитывает результат работы сети
        /// </summary>
        /// <returns>Возвращает массив, хронящий информацию о сработавших и не сработавших нейронах</returns>
        public bool[] CalcResult()
        {
            for(int k =0; k < this.size; k++)
            {
                this.neurons[k].MulInps();
                this.results[k] = this.neurons[k].Sum();
            }
            return this.results;
        }

        /// <summary>
        /// Сбрасывает все входы и результаты. Веса не меняются.
        /// </summary>
        public void Reset()
        {
            for (int k = 0; k < this.size; k++)
            {
                this.neurons[k].Reset();
                this.results[k] = false;
            }
        }


        /// <summary>
        /// Обучает всю сеть целиком.
        /// </summary>
        /// <param name="indexOfTrue">Индекс правильного ответа в таблице символов</param>
        public void Train(int indexOfTrue)
        {
            for (int i = 0; i < this.size; i++)
            {
                if (((this.results[i] == false) && i == indexOfTrue))
                {
                    this.neurons[i].incW();
                }
                else
                    if (this.results[i] == true && i != indexOfTrue)
                        this.neurons[i].decW();
            }
        }


        /// <summary>
        /// Обучение согласно Дельта-правилу
        /// </summary>
        /// <param name="indexOfTrue">Индекс правильного ответа</param>
        public void DeltaTrain(int indexOfTrue)
        {
            for (int k = 0; k < this.size; k++)
                if (k == indexOfTrue)
                {
                    if (this.results[k] != true)
                        this.neurons[k].DeltaTrain(1);
                }
                else
                    if (this.results[k] == true)
                        this.neurons[k].DeltaTrain(-1);
                    
        }
    }
}
