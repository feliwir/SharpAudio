for f in ./src/*/bin/Release/*.nupkg
do
    echo $f
    curl -vX PUT -u "feliwir:$GITHUB_TOKEN" -F package=@$f https://nuget.pkg.github.com/feliwir/
done