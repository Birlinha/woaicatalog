#!/bin/bash

# Variaveis
RESOURCE_GROUP="NomeDoResourceGroup"
LOCATION="brazilsouth"
API_MGMT_SERVICE_NAME="NomeDoAPIMgmtService"
COSMOS_DB_ACCOUNT_NAME="NomeDoCosmosDBAccount"
STORAGE_ACCOUNT_NAME="NomeDostorageaccount"

# Passo 1: Criar um Resource Group
az group create --name $RESOURCE_GROUP --location $LOCATION

# Passo 2: Criar uma API Management Service com plano de preço Consumption
az apim create \
    --name $API_MGMT_SERVICE_NAME \
    --resource-group $RESOURCE_GROUP \
    --location $LOCATION \
    --sku-name Consumption

# Passo 3: Criar um Azure Cosmos DB Account for NoSQL
az cosmosdb create \
    --name $COSMOS_DB_ACCOUNT_NAME \
    --resource-group $RESOURCE_GROUP \
    --location $LOCATION \
    --kind GlobalDocumentDB \
    --default-consistency-level Session

# Passo 4: Criar um Storage Account com  acesso Público a Blobs habilitado
az storage account create \
    --name $STORAGE_ACCOUNT_NAME \
    --resource-group $RESOURCE_GROUP \
    --location $LOCATION \
    --sku Standard_LRS \
    --kind StorageV2 \
    --enable-hierarchical-namespace true \
    --allow-blob-public-access true

# Passo 5: Criar containeres de imagem e video para armazenamento de arquivos
az storage container create \
    --name video \
    --account-name $STORAGE_ACCOUNT_NAME \
    --public-access blob

az storage container create \
    --name image \
    --account-name $STORAGE_ACCOUNT_NAME \
    --public-access blob