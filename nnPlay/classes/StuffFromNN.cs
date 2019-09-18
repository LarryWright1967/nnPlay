using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nnPlay
{
    class StuffFromNN
    {

        /* Copyright 2016 Google Inc. All Rights Reserved.

        Licensed under the Apache License, Version 2.0 (the "License");
        you may not use this file except in compliance with the License.
        You may obtain a copy of the License at

            http://www.apache.org/licenses/LICENSE-2.0

        Unless required by applicable law or agreed to in writing, software
        distributed under the License is distributed on an "AS IS" BASIS,
        WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
        See the License for the specific language governing permissions and
        limitations under the License.
        ==============================================================================*/

        //import* as d3 from 'd3';

        /// <summary>
        /// A two dimensional example: x and y coordinates with the label.
        /// </summary>
        public class Example2d
        {
            public double X { get; set; }
            public double Y { get; set; }
            public int Lable { get; set; }
            private double x;
            private double y;
            private int lable;
        }//good

        public class LPoint
        {
            public double X { get; set; }
            public double Y { get; set; }
            private double x;
            private double y;
        }//good

        /// <summary>
        /// Shuffles the array using Fisher-Yates algorithm. Uses the seedrandom
        /// library as the random generator.
        /// </summary>
        /// <param name="array"></param>
        public void Shuffle(ref Object[] array)
        {
            int counter = array.Length;
            object temp = 0;
            int index = 0;
            // While there are elements in the array
            while (counter > 0)
            {
                // Pick a random index
                double rVal = new Random().NextDouble();
                index = (int)Math.Floor(rVal * (double)counter);
                counter--;
                // And swap the last element with it
                temp = array[counter];
                array[counter] = array[index];
                array[index] = temp;
            }
        }//good


        public class NN
        {

            /// <summary>The collection of layers of nodes that defines the learning function.</summary>
            public List<List<Node>> Network = new List<List<Node>>();

            /* Copyright 2016 Google Inc. All Rights Reserved.
            Licensed under the Apache License, Version 2.0 (the "License");
            you may not use this file except in compliance with the License.
            You may obtain a copy of the License at
                http://www.apache.org/licenses/LICENSE-2.0
            Unless required by applicable law or agreed to in writing, software
            distributed under the License is distributed on an "AS IS" BASIS,
            WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
            See the License for the specific language governing permissions and
            limitations under the License.

            Credits
            This was created by Daniel Smilkov and Shan Carter. This is a continuation 
            of many people’s previous work — most notably Andrej Karpathy’s convnet.js 
            demo and Chris Olah’s articles about neural networks. Many thanks also to 
            D. Sculley for help with the original idea and to Fernanda Viégas and 
            Martin Wattenberg and the rest of the Big Picture and Google Brain teams 
            for feedback and guidance.
            ==============================================================================*/

            /// <summary>Builds a neural network.</summary>
            /// <param name="networkShape">The shape of the network. E.g. [1, 2, 3, 1] means the network will have one input node, 2 nodes in first hidden layer, 3 nodes in second hidden layer and 1 output node.</param>
            /// <param name="activation">The activation function of every hidden node.</param>
            /// <param name="outputActivation">The activation function for the output nodes.</param>
            /// <param name="regularization">The regularization function that computes a penalty for a given weight (parameter) in the network. If null, there will be no regularization.</param>
            /// <param name="inputIds">List of ids for the input nodes.</param>
            public List<List<Node>> BuildNetwork(List<int> networkShape, ActivationFunction activation, ActivationFunction outputActivation, RegularizationFunction regularization, List<string> inputIds, bool initZero = false)
            {
                int numLayers = networkShape.Count;
                int id = 1;

                /* List of layers, with each layer being a list of nodes. */
                List<List<Node>> network = new List<List<Node>>();

                for (int layerIdx = 0; layerIdx < numLayers; layerIdx++)
                {
                    bool isOutputLayer = layerIdx == numLayers - 1;
                    bool isInputLayer = layerIdx == 0;
                    List<Node> currentLayer = new List<Node>();
                    Network.Add(currentLayer);
                    int numNodes = networkShape[layerIdx];

                    for (int i = 0; i < numNodes; i++)
                    {
                        string nodeId = id.ToString();
                        if (isInputLayer)
                        {
                            nodeId = inputIds[i];
                        }
                        else
                        {
                            id++;
                        }
                        ActivationFunction act;
                        if (isOutputLayer) { act = outputActivation; } else { act = activation; }
                        Node node = new Node(nodeId, act, initZero);
                        currentLayer.Add(node);
                        if (layerIdx >= 1)
                        {
                            // Add links from nodes in the previous layer to this node.
                            for (int j = 0; j < network[layerIdx - 1].Count; j++)
                            {
                                Node prevNode = network[layerIdx - 1][j];
                                Link link = new Link(prevNode, node, regularization, initZero);
                                prevNode.Outputs.Add(link);
                                node.InputLinks.Add(link);
                            }
                        }
                    }

                }

                return network;
            }//good

            public void UpdateWeights(List<List<Node>> network, double learningRate, double regularizationRate)
            {
                for (int layerIdx = 1; layerIdx < network.Count; layerIdx++)
                {
                    List<Node> currentLayer = network[layerIdx];
                    for (int i = 0; i < currentLayer.Count; i++)
                    {
                        Node node = currentLayer[i];
                        // Update the node's bias.
                        if (node.NumAccumulatedDers > 0)
                        {
                            node.Bias -= learningRate * node.AccInputDer / node.NumAccumulatedDers;
                            node.AccInputDer = 0;
                            node.NumAccumulatedDers = 0;
                        }
                        // update the weights coming into this node.
                        for (int j = 0; j < node.InputLinks.Count; j++)
                        {
                            Link link = node.InputLinks[j];
                            if (link.isDead)
                            {
                                continue;
                            }
                            double regulDer = (link.regularization != null) ? link.regularization.Der(link.weight) : 0;
                            if (link.numAccumulatedDers > 0)
                            {
                                // Update the weight based on dE/dw.
                                link.weight = link.weight - (learningRate / link.numAccumulatedDers) * link.accErrorDer;
                                // Further update the weight based on regularization.
                                double newLinkWeight = link.weight - (learningRate * regularizationRate) * regulDer;
                                //if (link.regularization === RegularizationFunction.L1 && link.weight * newLinkWeight < 0)
                                //{
                                //    // The weight crossed 0 due to the regularization term. Set it to 0.
                                //    link.weight = 0;
                                //    link.isDead = true;
                                //}
                                //else
                                //{
                                //    link.weight = newLinkWeight;
                                //}
                                //    link.accErrorDer = 0;
                                //    link.numAccumulatedDers = 0;
                            }

                        }
                    }
                }
            }

            /// <summary>Iterates over every node in the network.</summary>
            public void ForEachNode(List<List<Node>> network, bool ignoreInputs, Node accessor, Node node)
            {
                int startIdx, layerIdx;
                if (ignoreInputs) { startIdx = 1; } else { startIdx = 0; }
                for (layerIdx = startIdx; layerIdx < network.Count; layerIdx++)
                {
                    List<Node> currentLayer = network[layerIdx];
                    for (int i = 0; i < currentLayer.Count; i++)
                    {
                        node = currentLayer[i];
                    }
                }
            }

            /// <summary>Returns the last node in the network.</summary>
            public Node GetOutputNode()
            {
                return Network[Network.Count - 1][0];
            }

        }

        /// <summary>
        /// A node in a neural network. Each node has a state
        /// (total input, output, and their respectively derivatives) which changes
        /// after every forward and back propagation run.
        /// </summary>
        public class Node // good
        {
            public string Id;

            /// <summary>List of input links.</summary>
            public List<Link> InputLinks = new List<Link>();
            public double Bias = 0.1;

            /// <summary>List of output links.</summary>
            public List<Link> Outputs = new List<Link>();

            /// <summary>Review; this may need to be changed to type int.</summary>
            public double TotalInput;

            /// <summary>Review; this may need to be changed to type int.</summary>
            public double Output;

            /// <summary>Error derivative with respect to this node's output.</summary>
            public double OutputDer;

            /// <summary>Error derivative with respect to this node's total input.</summary>
            public double InputDer;

            /// <summary>
            /// Accumulated error derivative with respect to this node's total input since
            /// the last update. This derivative equals dE/db where b is the node's
            /// bias term.
            /// </summary>
            public double AccInputDer = 0.0;

            /// <summary>
            /// Number of accumulated err. derivatives with respect to the total input
            /// since the last update.
            /// </summary>
            public double NumAccumulatedDers = 0.0;

            /// <summary>
            /// Activation function that takes total input and returns node's output.
            /// </summary>
            public ActivationFunction Activation = new ActivationFunction();

            /// <summary>
            /// Creates a new node with the provided id and activation function.
            /// </summary>
            /// <param name="id"></param>
            /// <param name="activation"></param>
            /// <param name="initZero"></param>
            public Node(string id, ActivationFunction activation, bool initZero = false)
            {
                this.Id = id;
                this.Activation = activation;
                if (initZero)
                {
                    this.Bias = 0;
                }
            }

            public double UpdateOutput()
            {
                // Stores total input into the node.
                ///
                this.TotalInput = this.Bias;
                for (int j = 0; j < this.InputLinks.Count; j++)
                {
                    Link link = this.InputLinks[j];
                    this.TotalInput += link.weight * link.source.Output;
                }
                this.Output = this.Activation.Output(this.TotalInput);
                return Output;
            }
        }

        public class ActivationFunction
        {
            public double Output(double x)
            {
                return Math.Tanh(x);
            }
            public double Der(double x)
            {
                double output = Output(x);
                return 1 - (output * output);
            }
        }

        public class RegularizationFunction
        {
            public double Output(double w)
            {
                return Math.Abs(w);
            }
            public double Der(double w)
            {
                double ww = w + w;
                if (ww == 0) return 0;
                if (ww < -1) return -1;
                if (ww > 1) return 1;
                return ww;
            }
        }

        public class ErrorFunction
        {
            //public static SQUARE ErrorFunction = { error: (output: number, target: number) => 0.5 * Math.pow(output - target, 2), der: (output: number, target: number) => output - target };
        }

        public class Link
        {
            /// <summary>Constructs a link in the neural network initialized with random weight.</summary>
            /// <param name="source">source The source node.</param>
            /// <param name="dest">@param dest The destination node.</param>
            /// <param name="regularization">regularization The regularization function that computes the penalty for this weight.If null, there will be no regularization.</param>
            /// <param name="initZero"></param>
            public Link(Node source, Node dest, RegularizationFunction regularization, bool initZero = false)
            {
                this.id = source.Id + "-" + dest.Id;
                this.source = source;
                this.dest = dest;
                this.regularization = regularization;
                if (initZero) { this.weight = 0.0; }
            }
            public string id;
            public Node source;
            public Node dest;
            public double weight = new Random().NextDouble() - 0.5;
            public bool isDead = false;
            /// <summary>Error derivative with respect to this weight.</summary>
            public double errorDer = 0.0;
            /// <summary>Accumulated error derivative since the last update.</summary>
            public double accErrorDer = 0.0;
            /// <summary>Number of accumulated derivatives since the last update.</summary>
            public double numAccumulatedDers = 0.0;
            public RegularizationFunction regularization = new RegularizationFunction();
        }



    }
}
