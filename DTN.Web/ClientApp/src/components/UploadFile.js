import React, { Component } from 'react';

export class UploadFile extends Component {
    static displayName = UploadFile.name;

    constructor(props) {
        super(props);
        this.state = { selectedFile: null, uploaded: false, loading: false, vehicles: [], mostOftenSoldVehicle: '' };
    }

    componentDidMount() {
    }

    onFileChange = event => {
        // Update the state
        this.setState({ selectedFile: event.target.files[0] });

    };

    onFileUpload = () => {

        this.setState({ uploaded: true });
        this.setState({ loading: true });

        // Create an object of formData
        const formData = new FormData();

        // Update the formData object
        formData.append(
            "File",
            this.state.selectedFile,
            this.state.selectedFile.name
        );

        fetch('/api/vehicle/upload',
            {
                method: 'POST',
                body: formData,
            }
        )
            .then((response) => response.json())
            .then((result) => {
                if (result.isSucceeded === true) {                    

                    fetch('/api/vehicle/get')
                        .then((res) => res.json())
                        .then((vehicleResult) => {

                            console.log('data', vehicleResult.result.list);

                            this.setState({ vehicles: vehicleResult.result.list, loading: false  });
                            this.setState({ mostOftenSoldVehicle: vehicleResult.result.mostOftenSoldVehicle });
                        });
                }
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    };

    fileData = () => {

        if (this.state.uploaded === true) {

            return (
                <div>
                    <div class="alert alert-success" role="alert">
                        {this.state.selectedFile.name} successfully uploaded
                    </div>
                    <div class="alert alert-primary" role="alert">
                        The most often sold vehicle : <strong>{this.state.mostOftenSoldVehicle}</strong>
                    </div>
                </div>
            );
        } else {
            return (
                <div class="form-group row">
                    <h1 id="tabelLabel" >Upload a vehcile file!</h1>
                    <p>Upload vehicle sales data file to visualize the data.</p>
                    <div class="form-group">
                        <input type="file" onChange={this.onFileChange} class="form-control-file" />

                    </div>
                    <div class="form-group">
                        <small id="passwordHelpBlock" class="form-text text-muted">
                            <span>Maximum allowed file size is 1MB. </span>
                            <span>Accepted file types : .csv</span>
                        </small>
                    </div>
                    <div class="form-group">
                        <button onClick={this.onFileUpload} class="btn btn-success btn-s"> <span class="glyphicon glyphicon-upload"></span>Upload File</button>
                    </div>

                </div>
            );
        }
    };

    render() {
        return (
            <div class="container">
                {this.fileData()}
            </div>
        );
    }
}
