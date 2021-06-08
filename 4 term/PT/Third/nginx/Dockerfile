FROM nginx:alpine

RUN mkdir -p /usr/src/app/

WORKDIR /usr/src/app/
COPY nginx.conf /etc/nginx

ENTRYPOINT nginx -g 'daemon off;'
