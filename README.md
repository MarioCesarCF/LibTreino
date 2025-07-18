# Documentação da API

## Objetivo

Esta API tem como objetivo gerenciar os dados salvos no banco de dados, fornecendo endpoints para cadastro, acesso e manipulação de informações.

## Fluxo de Uso

**1. Cadastro de Usuário:** O usuário cria uma conta utilizando e-mail, senha, nome completo e número de telefone.

**2. Login:** O usuário acessa a plataforma informando suas credenciais.

**3. Tela Principal:**

* Visualização de listas cadastradas (limite de até 3 listas).

* Edição de listas existentes.

* Criação de nova lista.

* Acesso às opções de gerenciamento de amigos.

**4. Gerenciamento de Lista:**

* Adicionar um novo item.

* Alterar a quantidade de um item.

* Excluir uma lista.

* Editar o nome da lista.

* Adicionar amigos à lista.

* Remover um amigo da lista.

**5. Gerenciamento de Amigos:**

* Adicionar um novo amigo por meio de um código.

* Remover um amigo.

* Visualizar a lista de amigos cadastrados na plataforma.

## **Estrutura das Entidades**

### **Usuário**

* **id (string)** - Identificador único do usuário.

* **nome_completo (string)** - Nome completo do usuário.

* **email (string)** - Endereço de e-mail do usuário.

* **senha (string)** - Senha de acesso.

* **n_telefone (string)** - Número de telefone.

* **codigo (string)** - Código único do usuário para conexão com amigos.

### Lista de Produtos

* **id (string)** - Identificador único da lista.

* **usuario_id (string)** - Referência ao usuário dono da lista.

* **nome (string)** - Nome da lista.

* **produtos (array de objetos):**

  * **nome (string)** - Nome do produto.

  * **quantidade (number)** - Quantidade do produto.

  * **status (boolean)** - Indica se o produto foi adquirido.

### Amizade

* **id (string)** - Identificador único do relacionamento.

* **usuario_id (string)** - Referência ao usuário que fez a conexão.

* **amigo_id (string)** - Referência ao amigo conectado.

## Tecnologias Utilizadas

* **Linguagem:** C#

* **Framework:** ASP.Net

* **Banco de Dados:** MongoDB ou PostgreSQL (a definir)

## Endpoints da API

### Autenticação

* **POST /api/user** - Cadastro de novo usuário.

* **POST /api/user/login** - Autenticação e geração de token.

### Usuários

* **GET /api/user/{id}** - Obtém informações do usuário.

* **PUT /api/user/{id}** - Atualiza dados do usuário.

* **DELETE /api/user/{id}** - Exclui o usuário.

### Listas

* **POST /api/shoppinglistr** - Cria uma nova lista.

* **GET /api/shoppinglist** - Obtém todas as listas do usuário.

* **GET /api/shoppinglist/{id}** - Obtém detalhes de uma lista específica.

* **PUT /api/shoppinglist/{id}** - Atualiza uma lista.

* **DELETE /api/shoppinglist/{id}** - Exclui uma lista.

### Produtos

* **POST /api/product** - Adiciona um novo produto à lista.

* **PUT /api/product/{id}** - Atualiza a quantidade ou status de um produto.

* **DELETE /api/product/{id}** - Remove um produto da lista.

(Para ser implementadas)
### Amigos

* **POST /amigos** - Adiciona um amigo por código.

* **GET /amigos** - Lista todos os amigos do usuário.

* **DELETE /amigos/{id}** - Remove um amigo.

## Autenticação e Segurança

* Autenticação via JWT (JSON Web Token).

* Todas as requisições protegidas por middleware de segurança.

* Criptografia de senhas usando bcrypt.
