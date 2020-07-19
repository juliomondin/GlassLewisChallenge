# Glass Lewis Challenge
This code was created to satisfy a code challenge desenvolved by Glass Lewis.
It has 3 projects, one containing a WebAPI, one containing Unit tests from the API,
and the last one containing integrated tests from end-to-end.


To run the project, please go to GlassLewisChallenge/GlassLewisChallenge and run docker-compose up


Make sure you have docker and Dot.net core 2.2 on your machine.


Project will use the following ports:  1433, 8080, please make sure nothing is running
on these ports.

To run swagger, please go to localhost:8080/swagger



#Authentication

The project has jwt bearer authentication, to authenticate please send a POST requisition
to localhost:8080/login with the following payload.

{
"Username": "GlassLewis",
"Password": "123"
}



## Contact
jmondinjr@gmail.com