docker build -t web .
docker run -e "ASPNETCORE_URLS=http://+:5000" -P web