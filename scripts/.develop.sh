#!/bin/bash

export RANCHER_STACK=dev

# deploy WebAPI
export RANCHER_SERVICE=scd
export IMAGE_NAME=$DOCKER_IMAGE-dev:$DOCKER_TAG
. ./scripts/.deploy.sh
