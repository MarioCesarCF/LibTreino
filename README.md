## Documentação
O objetivo desta API é fazer o gerenciamento dos dados salvos no banco de dados, fornecendo end-points para salvar, acessar e manipular os dados.

### Descrição geral - Fluxo de uso
* O usuário se cadastra na plataforma, utilizando e-mail, senha, nome completo, número de telefone.
* Na página principal, após login, o usuário terá as seguintes opções, ver as listas cadastradas (no máximo 3), clicar nas listas para editar, criar uma nova lista e ver amigos. 
* Ao clicar na lista o usuário poderá adicionar novo item, alterar quantidade de algum item, deletar a lista, editar nome da lista ou adicionar amigo na lista.
* Na página para ver amigos o usuário poderá adicionar um novo amigo, usando o código ou remover algum amigo da lista. Além de ver o nome de todos os seus amigos na plataforma.

### Propriedades das entidades
* Usuario: nome_completo, email, senha, n_telefone, codigo
* Lista_produtos: [{nome, quantidade, status}]
