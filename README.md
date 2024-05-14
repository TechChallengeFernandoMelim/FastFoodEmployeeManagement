# FastFoodEmployeeManagement

O repositorio FastFoodEmployeeManagement tem por objetivo implementar uma Lambda Function respons�vel por realizar a cria��o e autentica��o de funcion�rios da lanchonete utilizando o AWS Cognito.


### Vari�veis de ambiente
Todas as vari�veis de ambiente do projeto visam fazer integra��o com algum servi�o da AWS. Explicaremos a finalidade de cada uma:

- AWS_ACCESS_KEY_DYNAMO: "Access key" da AWS. Recurso gerado no IAM para podermos nos conectar aos servi�os da AWS;
- AWS_SECRET_KEY_DYNAMO: "Secret key" da AWS. Recurso gerado no IAM para podermos nos conectar aos servi�os da AWS. Deve ser utilizado corretamente com seu par AWS_ACCESS_KEY_DYNAMO;
- AWS_TABLE_NAME_DYNAMO: Nome da tabela de usu�rios cadastrada no DynamoDB;
- AWS_EMPLOYEE_POOL_ID: Nome da Employee pool criada no AWS Cognito;
- AWS_CLIENT_ID_COGNITO: ClientId da pool no AWS Cognito;
- GUEST_EMAIL: Usu�rio padr�o para realizar autentica��o de forma an�nima no AWS Cognito;
- GUEST_IDENTIFICATION: senha do usu�rio padr�o para realizar autentica��o de forma an�nima no AWS Cognito.
- AWS_SQS: Url da fila de log no SQS da AWS.
- AWS_SQS_GROUP_ID: Group Id da fila de log no SQS da AWS.

### Execu��o com Docker

A execu��o do projeto pode ser feita buildando o dockerfile na raiz do reposit�rio e depois executando a imagem gerada em um container. O servi�o foi testado sendo executado direto pelo visual Studio e pela AWS.

### Testes

Conforme foi solicitado, estou postando aqui as evid�ncias de cobertura dos testes. A cobertura foi calculada via integra��o com o [SonarCloud](https://sonarcloud.io/) e pode ser vista nesse [link](https://sonarcloud.io/organizations/techchallengefernandomelim/projects). A integra��o com todos os reposit�rios poder� ser vista nesse link.

![Coverage1](./images/coverage1.png)

![Coverage2](./images/coverage2.png)

Atrav�s das imagens � poss�vel observar que a cobertura por testes unit�rios ficou superior a 80%, conforme solicitado.

### Endpoints

Os endpoints presentes nesse projeto s�o:

- POST Employee/CreateEmployee: Respons�vel por criar as credenciais de login de um funcion�rio.
- GET Employee/AuthenticateEmployee: Respons�vel por autenticar o funcion�rio com email e senha.
- GET Employee/GetEmployees: Respons�vel por retornar todos os funcion�rios cadastrados.
- GET Employee/NotImplementedException: Endpoint apenas para testar se servi�o de log est� funcionando.