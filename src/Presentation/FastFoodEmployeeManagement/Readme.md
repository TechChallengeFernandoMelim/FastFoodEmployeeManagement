# FastFoodEmployeeManagement

O repositorio FastFoodEmployeeManagement tem por objetivo implementar uma Lambda Function respons�vel por realizar a cria��o e autentica��o de funcion�rios da lanchonete utilizando o AWS Cognito.


### Vari�veis de ambiente
Todas as vari�veis de ambiente do projeto visam fazer integra��o com algum servi�o da AWS. Explicaremos a finalidade de cada uma:

- AWS_ACCESS_KEY_DYNAMO: "Access key" da AWS. Recurso gerado no IAM para podermos nos conectar aos servi�os da AWS;
- AWS_SECRET_KEY_DYNAMO: "Secret key" da AWS. Recurso gerado no IAM para podermos nos conectar aos servi�os da AWS. Deve ser utilizado corretamente com seu par AWS_ACCESS_KEY_DYNAMO;
- AWS_TABLE_NAME_DYNAMO: Nome da tabela de usu�rios cadastrada no DynamoDB;
- LOG_REGION: Regi�o do Log Group criado no Cloudwatch para monitoramento de logs;
- LOG_GROUP: Nome do Log Group criado no Cloudwatch para monitoramento de logs;
- AWS_EMPLOYEE_POOL_ID: Nome da Employee pool criada no AWS Cognito;
- AWS_CLIENT_ID_COGNITO: ClientId da pool no AWS Cognito;
- GUEST_EMAIL: Usu�rio padr�o para realizar autentica��o de forma an�nima no AWS Cognito;
- GUEST_IDENTIFICATION: senha do usu�rio padr�o para realizar autentica��o de forma an�nima no AWS Cognito.

### Execu��o com Docker

A execu��o do projeto pode ser feita buildando o dockerfile na raiz do reposit�rio e depois executando a imagem gerada em um container. O servi�o foi testado sendo executado direto pelo visual Studio e pela AWS.

### Testes

