docker network create productmanagementsystem --label=productmanagementsystem
docker-compose -f docker-compose.infrastructure.yml up -d
exit $LASTEXITCODE