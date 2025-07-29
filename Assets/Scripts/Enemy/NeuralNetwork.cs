using System;
using UnityEngine;

public class NeuralNetwork
{
    private int[] layers;
    private float[][] neurons;
    private float[][][] weights;
    private System.Random random;

    public NeuralNetwork(int[] layers)
    {
        this.layers = layers;
        this.random = new System.Random();

        InitializeNeurons();
        InitializeWeights();
    }

    private void InitializeNeurons()
    {
        neurons = new float[layers.Length][];
        for (int i = 0; i < layers.Length; i++)
        {
            neurons[i] = new float[layers[i]];
        }
    }

    private void InitializeWeights()
    {
        weights = new float[layers.Length - 1][][];

        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = new float[neurons[i + 1].Length][];
            for (int j = 0; j < weights[i].Length; j++)
            {
                weights[i][j] = new float[neurons[i].Length];
                for (int k = 0; k < weights[i][j].Length; k++)
                {
                    weights[i][j][k] = (float)random.NextDouble();
                }
            }
        }
    }

    public float[] FeedForward(float[] inputs)
    {
        // Asigna los valores de entrada a las neuronas de la primera capa
        for (int i = 0; i < inputs.Length; i++)
        {
            neurons[0][i] = inputs[i];
        }

        // Propagación hacia adelante a través de cada capa de la red
        for (int i = 1; i < layers.Length; i++)
        {
            for (int j = 0; j < neurons[i].Length; j++)
            {
                float value = 0f;

                for (int k = 0; k < neurons[i - 1].Length; k++)
                {
                    value += weights[i - 1][j][k] * neurons[i - 1][k];
                }

                neurons[i][j] = (float)Math.Tanh(value); // Activa con función tangente hiperbólica

            }
        }

        // Devuelve la última capa como salida
        return neurons[layers.Length - 1];
    }
}
