#!/bin/sh

if [ -z "${listen}" ]; then
listen="8080"
fi

if [ -z "${proxy_pass}" ]; then
proxy_pass="http://0.0.0.0:8000"
fi

sed -i -e "s%LISTEN%${listen}%g" /etc/nginx/nginx.conf
sed -i -e "s%PROXY_PASS%${proxy_pass}%g" /etc/nginx/nginx.conf

nginx -g 'daemon off;'
