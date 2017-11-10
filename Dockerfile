FROM microsoft/dotnet:2.0-runtime-deps

COPY SCD/Web/publish /home/bin
WORKDIR /home/bin

EXPOSE 1806/tcp

CMD ["./Prodest.Scd.Web"]
