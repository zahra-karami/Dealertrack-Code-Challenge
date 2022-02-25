import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div className="jumbotron">
                <h1>Hello!</h1>
                <p>Welcome to your new single-page application, built with:</p>
                <ul>
                    <li><a href='https://get.asp.net/'>ASP.NET Core 6.0</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C# 10.0</a> for cross-platform server-side code</li>
                    <li><a href='https://facebook.github.io/react/'>React</a> for client-side code</li>
                    <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
                    <li><a href='https://aws.amazon.com/dynamodb/'>AWS DynamoDB</a> for storing the vehicle sale records</li>
                    <li><a href='https://aws.amazon.com/cloudwatch/'>AWS CloudWatch</a> as sink for logs and error handling</li>
                </ul>
                <h2>To start click on button below!</h2>
                <p class="lead">
                    <a class="btn btn-success btn-lg" href="/fetch-vehicles">Start Demo!</a>
                </p>
                <hr class="my-4" />
                <h4>AWS Credential:</h4>
                <div>
                    Download your credential for 'awscoxautolabs220' or 'awsdtncanadal2np' from ALKS.
                    Then, save it in at 'C:\Users\your-username\.aws\credentials' as [default] profile.
                </div>
                <hr class="my-4" />
                <h4>Local Docker Build and Run:</h4>
                <div>                   
                    <p>on windows run this command from project directory after building the project: </p>
                    <code>ddocker-compose -f "docker-compose.yml" -f "docker-compose.override.yml" -f "./obj/Docker/docker-compose.vs.debug.g.yml" -p dockercompose-dtn --ansi never up -d </code>
                </div>

                <hr class="my-4" />
                <h4>Here is a guid for Demo:</h4>
                <div className="lead">
                    <div>- To successful demo you need set creditial for <code>awscoxautolabs220</code> or  <code>awsdtncanadal2np</code> server on your local machine</div>
                    <div>- Click on <cite>'Demo!'</cite>. In this page, you can see a list of vehicle sale records that have been read from AWS DynamoDB table named <code>'DealerTrackVehicles'</code></div>
                    <div>- Then, you can Delete records from server or upload a new file </div>
                    <div>- To upload a new file click on <cite>'Upload New Vehicle'</cite> on demo page.</div>
                    <div>- After uploading a new file, it will show a list of records in file and It will highlight <cite>Duplicated</cite> record in <code>RED</code> (Duplicated recod will be detected base on dealNumber)</div>
                    <div>- Log and error handling sink in AWS CloudWatch (AWS CloudWatch loggroup name: <code>'/dotnet/dealertrack/api/np')</code></div>
                </div>
            </div>
        );
    }
}
