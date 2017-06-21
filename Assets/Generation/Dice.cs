using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

//retorna numero real de 0 a 1


	class Dice
	{
		private Random _rnd;
		private float _result;


		//vamos declarar as probabilidades
		public Dice(int seed)
		{
			_rnd = new Random(seed);
			_result = 0;

		}


		public float roll()
		{
			_result = (float)_rnd.Next(0, 100)/100;

			return _result;
		}


	}

