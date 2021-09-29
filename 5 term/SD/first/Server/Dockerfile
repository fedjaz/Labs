FROM openjdk as build
ENV APPDIR /home/src/Server

RUN mkdir -p $APPDIR
COPY . $APPDIR

WORKDIR $APPDIR
RUN ./gradlew build

FROM openjdk as run
ENV APPDIR /home/src/Server

WORKDIR $APPDIR

COPY --from=build $APPDIR/build/libs/server-0.0.1-SNAPSHOT.jar $APPDIR/app.jar
CMD ["java", "-jar", "app.jar"]

