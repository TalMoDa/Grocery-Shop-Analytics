FROM node:lts-slim AS build
WORKDIR /src

RUN npm cache clean --force

RUN npm config set strict-ssl false

RUN npm install -g @angular/cli

COPY package*.json ./
RUN npm ci

COPY . ./
RUN ng build --configuration=production

FROM nginx:stable AS final
COPY --from=build /src/dist/shop-analytics-client/browser/ /usr/share/nginx/html
COPY /nginx.conf  /etc/nginx/conf.d/default.conf

EXPOSE 80
