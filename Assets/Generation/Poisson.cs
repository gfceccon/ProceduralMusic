using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


	public class Poisson
	{
		private int _lambda;

		public Poisson(int lambda)
		{
			_lambda = lambda;
		}

		public void setLambda(int lambda){

			_lambda = lambda;
		}



		//pega apenas uma probabilidade
		public double getProb(int kParam){
			
			double prob = 0;
			
			//vamos querer a probabilidade a partir do meio
			kParam = kParam + _lambda;
				//Console.WriteLine(kParam);

			double fatorial = 1;
			if(kParam != 0){
				fatorial = kParam;
			}
			for(int i = kParam -1; i>0 ; i--){
				fatorial = fatorial*i;
				//Console.WriteLine(fatorial);
				
			}

			prob = Math.Pow(2.7182818284590451, -_lambda)*(Math.Pow(_lambda, kParam)/fatorial);

			return prob;


		}




	}
