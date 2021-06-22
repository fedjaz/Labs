FROM nginx:alpine

ADD nginx.conf /etc/nginx
ADD run.sh /etc/nginx
RUN chmod +x /etc/nginx/run.sh

CMD /etc/nginx/run.sh
