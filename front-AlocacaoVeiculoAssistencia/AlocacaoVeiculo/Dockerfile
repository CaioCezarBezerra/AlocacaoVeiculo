FROM node:22-alpine AS build

WORKDIR /app

COPY package*.json ./

RUN npm ci

COPY . .

RUN npm run build


FROM nginx:alpine AS final

COPY nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist /tmp/dist

RUN set -eux; \
    INDEX_FILE="$(find /tmp/dist -type f -name index.html | head -n 1)"; \
    test -n "$INDEX_FILE"; \
    BUILD_DIR="$(dirname "$INDEX_FILE")"; \
    cp -r "$BUILD_DIR"/. /usr/share/nginx/html/

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]