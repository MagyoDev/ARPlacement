# ARPlacement
Este repositório contém o código-fonte para uma aplicação de realidade aumentada (AR) que permite ao usuário colocar objetos em um plano detectado pelo dispositivo.

## PlaceOnPlane

O script `PlaceOnPlane` é responsável por controlar o posicionamento dos objetos no plano detectado pelo AR. Ele utiliza o `ARRaycastManager` para lançar raios AR e detectar o plano em que o objeto será colocado. Algumas funcionalidades importantes do script são:

- Instanciação de objetos: O prefab do objeto a ser colocado é instanciado no local em que o toque é detectado.
- Reposicionamento de objetos: Se o objeto já tiver sido colocado anteriormente, ele será reposicionado para o novo local de toque.
- Rotação de objetos: Ao tocar com dois dedos na tela, é possível rotacionar o objeto em torno de seu eixo vertical.

## PlacementIndicator

O script `PlacementIndicator` é responsável por exibir um indicador visual para auxiliar o usuário na colocação dos objetos. Ele utiliza o `ARRaycastManager` para lançar um raio AR a partir do centro da tela e determinar a posição e rotação do indicador de acordo com o plano detectado. Algumas funcionalidades importantes do script são:

- Atualização do indicador visual: O indicador é atualizado para a posição e rotação do plano detectado.
- Ocultação do indicador: Se nenhum plano for detectado, o indicador visual é ocultado.

## Instruções de uso

1. Certifique-se de ter um dispositivo compatível com ARCore ou ARKit.
2. Abra a aplicação em seu dispositivo.
3. Posicione o dispositivo para detectar um plano na área de visualização.
4. Toque na tela para colocar um objeto no plano detectado.
5. Se desejar rotacionar o objeto, toque com dois dedos na tela e mova-os para girar o objeto.

Divirta-se explorando a realidade aumentada e colocando objetos virtuais no mundo real!
