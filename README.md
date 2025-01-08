# Azure Project

Este projeto utiliza tecnologias do Azure para implementar uma solução escalável e eficiente, baseada em:
- **Azure Storage**: Armazenamento seguro e escalável de dados.
- **Azure Functions**: Processamento serverless para eventos e lógica de negócios.
- **Azure CosmosDB**: Banco de dados NoSQL de alta performance.

## Estrutura do Projeto

A arquitetura do projeto é apresentada na imagem abaixo:

![Infraestrutura do Projeto](infra.png)

### Fluxo do Projeto
1. Os dados enviados ao portal (imagens e vídeos) são armazenados no **Azure Storage**.
2. As **Azure Functions** processam os eventos gerados e inserem metadados no **Azure CosmosDB**.
3. A aplicação final consolida os dados, permitindo visualização e interatividade através de uma página web.

![Exemplo de Dados](data_example.png)

![Página Final do Projeto](final_project.png)

## Arquivos Relevantes

### `movies_preview.html`
Página principal da solução, que integra o resultado do projeto. Permite visualizar imagens e vídeos armazenados, com um preview funcional de vídeos (conforme mostrado em `final_project.png`).

### `postmanCollection.json`
Coleção do Postman que documenta e permite a reprodução das requisições utilizadas no projeto.

### `infra_deploy.sh`
Script que automatiza o provisionamento da arquitetura, configurando os serviços necessários no Azure.

## Como Executar
1. Configure sua conta do Azure e certifique-se de que os recursos necessários estão disponíveis.
2. Execute o script `infra_deploy.sh` para inicializar a infraestrutura.
3. Utilize a coleção do Postman (`postmanCollection.json`) para testar as funções.
4. Acesse a página `movies_preview.html` para visualizar o resultado final do projeto.

## Tecnologias Utilizadas
- Azure Storage
- Azure Functions
- Azure CosmosDB
- HTML5
- Shell Script
- Postman