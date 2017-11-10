#!/bin/bash

export RANCHER_STACK=hmg

# deploy WebAPI
export RANCHER_SERVICE=scd
export IMAGE_NAME=$DOCKER_IMAGE-hmg:$DOCKER_TAG
. ./scripts/.deploy.sh
