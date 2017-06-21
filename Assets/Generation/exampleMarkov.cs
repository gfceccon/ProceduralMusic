using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

//programa lera uma cadeia de markov e andara por ela
//da suas probabilidades de transicao
public enum ENote
{
	C = 0,
	Db,
	D,
	Eb,
	E,
	F,
	Gb,
	G,
	Ab,
	A,
	Bb,
	B
};


	class Program
	{
		static void Main(string[] args)
		{
			//cria um dado
			Dice dice = new Dice(666969);
			int numOfStates = 12;

			//alterando lambda conseguimos exterder a curva ou comprimi-la
			int lambda = 8;

			// cria a cadeia de markov, com 10 estados/
			MarkovChain markov = new MarkovChain(numOfStates, lambda);

			//mostra a cadeia
			markov.showMChain(ENote.C);
			markov.showMChain(ENote.D);
			markov.showMChain(ENote.E);
			markov.showMChain(ENote.F);
			markov.showMChain(ENote.G);
			markov.showMChain(ENote.A);
			markov.showMChain(ENote.B);
			
			
			List<double[]> chain = markov.Chain;

			int[] hits = new int[numOfStates];
			Console.WriteLine("quantas vezes cada nota foi acessada:\n ordem: C D E F G A B...");
			for (int i = 0; i <numOfStates ; i++) {
				for (int j = 0; j < numOfStates; j++){
					hits[j] = 0;
				}

				for (int k = 0; k < 200; k++)
				{
					hits[markov.walkThrough((float)dice.roll(), (ENote)i)]++;
					//Console.WriteLine(markov.walkThrough((float)dice.roll() / 100, (ENote)i));

				}

				foreach(int hit in hits){
					Console.Write(hit + " ");
				}
				Console.WriteLine();
			}

			Console.ReadLine();
		}

	}


