## Solução em c#
![Csharp](https://raw.githubusercontent.com/ricardovicentini/Algoritmos/master/c%23.jpg)
**Program.cs**
Arquivo de teste onde eh feita a solicitação do valor desejado para saque
E na sequência é instânciada a classe CaixaEletrônico
**CaixaEletronico.cs**
Classe que comtém a regra de negógico CalcularNotas\
CalcularNotas, recebe o valor a ser sacado e calcula as notas possíveis para o valor informado
**Notas.cs**
Classe que simboliza uma nota do mundo real
**Solução Proposta**
O valor solicitado é enviado para CalcularNotas, que calcula a quantidade de notas possíveis deixando saldo remanescente, o saldo remanescente e enviado para calculo de notas para notas de uma grandeza menor e assim sussecivamente.\
###Exemplo###
Solicitado saque de 140 reais\
CalcularNotas para notas de 100, fica com saldo de 40 reais\
Passo o saldo para calcular com notas de 50, o saldo se mantém em 40 já que não é possível obter nenhuma nota de 50\
Passo o saldo para clacular com notas de 20, o saldo fica zerado, e um List com 3 notas e retornado para a classe de teste Program.cs\
Caso ainda haja saldo mesmo apos calcular com notas de 20, significa que o valor solicitado para saque é inválido\
Para imprimir o resultado com maior clareza utilizo Linq para agrupar as notas e apresentar na tela
