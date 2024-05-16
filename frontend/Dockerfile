FROM node:20.9.0-alpine

WORKDIR /frontend

COPY . ./

RUN npm install

RUN npm run build

EXPOSE 8080

ENTRYPOINT ["npm", "run", "start"]