FROM node:iron-alpine3.18@sha256:53108f67824964a573ea435fed258f6cee4d88343e9859a99d356883e71b490c AS base

USER node
WORKDIR /app

FROM base AS builder
COPY --chown=node:node package*.json ./
RUN npm ci
COPY --chown=node:node . .

FROM builder AS dev
EXPOSE 5173
ENTRYPOINT ["npm", "run", "dev", "--", "--host", "0.0.0.0"]

FROM builder AS prod
RUN npm run build
RUN npm prune --omit=dev
ENTRYPOINT ["node", "build/index.js"]

FROM base
ENV NODE_ENV=production
WORKDIR /app
COPY --from=prod /app/node_modules ./node_modules
COPY --from=prod /app/build ./build

COPY package.json package-lock.json ./
EXPOSE 3000
RUN npm run build
ENTRYPOINT ["node", "build/index.js"]