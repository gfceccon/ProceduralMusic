using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


	class MarkovChain
	{
		private List<double[]> _chain = new List<double[]>();

		private int _numOfStates;


		public List<double[]> Chain
		{
			get { return _chain; }
		}


		//dado um lambda e o numero de notas, monta uma tabela de probabilidades de poisson
		public MarkovChain(int numOfStates, int lambda)
		{
			//poisson
			_numOfStates = numOfStates;

			Poisson poisson = new Poisson(lambda);


			double[] states;

			for (int i = 0; i < _numOfStates; i++)
			{

				states = new double[_numOfStates];
				states[0] = poisson.getProb(-i);
				for (int j = 1; j < _numOfStates; j++)
				{
					//descomentar um e comentar o outro, mas o primeiro serve para calcular a prob, o segundo apenas pra visualizacao

					states[j] = states[j - 1] + poisson.getProb(j - i);   //diferenca para calcular prob
				  //states[j] =  poisson.getProb(j-i); //probabilidade real 
												

				}
				_chain.Add(states);
			}
		}


		//dado uma nota, mostra a probabilidade dela
		public bool showMChain(ENote note)
		{
			if (_chain != null)
			{
				foreach (double NoteProb in _chain[(int)note])
				{

					Console.Write(NoteProb.ToString("0.00") + " ");
				}
				Console.WriteLine();
				return true;
			}
			else return false;

		}

		//dado uma nota e um numero aleatorio de 0 a 1, calcula qual nota foi escolhida
		public int walkThrough(double numberSorted, ENote note)
		{
			//faz as interacoes, por cada dado rodad
			double[] states = _chain[(int)note];
			//condicao de inicio
			if (numberSorted < states[0] / states[_numOfStates - 1])
			{
				//Console.Write("0 ");
				// Console.Write("numero do dado: "+ numberSorted.ToString() +"\n0 \n");
				return 0;
			}
			else
			{
				for (int i = 1; i < _numOfStates; i++)
				{
					if (numberSorted <= states[i] / states[_numOfStates - 1] && numberSorted > states[i - 1] / states[_numOfStates - 1])
					{
						//Console.Write(i.ToString() + " ");
						//Console.Write("numero do dado: "+ numberSorted.ToString() + "\n" + i.ToString() + "\n");
						return i;
					}
				}
			}

			return -1;
		}

	}

