#!/bin/bash

# Variaveis
RESOURCE_GROUP="INDIE_RSG"
LOCATION="brazilsouth"
API_MGMT_SERVICE_NAME="INDIE-APIMGMT"
COSMOS_DB_ACCOUNT_NAME="indie-cosmodb"
STORAGE_ACCOUNT_NAME="indie0storage"

# Passo 1: Criar um Resource Group
az group create --name $RESOURCE_GROUP --location $LOCATION

# Passo 2: Criar uma API Management Service com plano de preço Consumption
az apim create \
    --name $API_MGMT_SERVICE_NAME \
    --resource-group $RESOURCE_GROUP \
    --location $LOCATION \
    --sku-name Consumption \
    --publisher-email youremail@address.com --publisher-name yourname

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