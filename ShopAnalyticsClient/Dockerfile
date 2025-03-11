# Use Node.js image to build the Angular app
FROM node:18 AS build
WORKDIR /app

# Copy package.json and package-lock.json first
COPY package*.json ./

# Clean install dependencies using --force to ensure correct platform binaries
# This forces npm to install platform-specific binaries for the current environment (Linux)
RUN npm ci --force

# Copy the entire source code after installing dependencies
COPY . .

# Build the Angular app
RUN npm run build --configuration=production

# Use Nginx to serve the Angular app
FROM nginx:alpine

# Copy the built app to nginx html directory
COPY --from=build /app/dist/shop-analytics-client /usr/share/nginx/html

# Optional: Copy custom nginx configuration if needed
# COPY nginx.conf /etc/nginx/conf.d/default.conf

# Expose the port Nginx runs on
EXPOSE 80

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]
