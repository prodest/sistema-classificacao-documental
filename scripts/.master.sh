#!/bin/bash

export RANCHER_STACK=prd

# deploy WebAPI
export RANCHER_SERVICE=scd
export IMAGE_NAME=$DOCKER_IMAGE-prd:$DOCKER_TAG
. ./scripts/.deploy.sh
