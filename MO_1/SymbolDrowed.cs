
namespace MO_1
{
    /// <summary>
    /// Перевод графического изображение в формат понятный нейронной сети
    /// </summary>
    class SymbolDrowed
    {
        /// <summary>
        /// Представление изображения.
        /// </summary>
        private int[,] image;

        /// <summary>
        /// Определяет возможность работы с текущим представлением изображения.
        /// </summary>
        public bool canIndicate = false;


        /// <summary>
        /// Конструктор, по умолчанию. Ничего интересного не делает.
        /// </summary>
        public SymbolDrowed()
        {
            this.image = new int[10,10];
        }


        /// <summary>
        /// Отчищает текущее представление символа.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    this.image[i,j] = 0;
        }


        /// <summary>
        /// Задаёт значение определённого пикселя на картинке.
        /// </summary>
        /// <param name="x">Координата X пикселя</param>
        /// <param name="y">Координата Y пикселя</param>
        public void SetAtXY(int x, int y)
        {
            if(x>=0 && y>=0 && x<10 && y< 10)
                this.image[x,y] = 1;
            this.canIndicate = true;
        }

        /// <summary>
        /// Отчищает определённый пиксель.
        /// </summary>
        /// <param name="x">Координата X пикселя</param>
        /// <param name="y">Координата Y пикселя</param>
        public void ClearAtXY(int x, int y)
        {
            this.image[x,y] = 0;
        }

        /// <summary>
        /// Возвращает изображение в понятном для сети формате.
        /// </summary>
        /// <returns>Числовой массив отображающий состояние сети.</returns>
        public int[,] ReturnXs()
        {
            return this.image;
        }
    }
}
