FROM node:12-alpine as builder
WORKDIR /app

COPY clients-service/package.json /app/package.json
RUN npm install --only=prod

COPY /clients-service /app
RUN npm run build

FROM nginx:1.16.0-alpine
COPY --from=builder /app/build /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]