import React, { Component } from 'react';

export class UploadFile extends Component {
    static displayName = UploadFile.name;

    constructor(props) {
        super(props);
        this.state = { selectedFile: null };
    }

    componentDidMount() {
    }

    onFileChange = event => {
        // Update the state
        this.setState({ selectedFile: event.target.files[0] });

    };

    onFileUpload = () => {

        // Create an object of formData
        const formData = new FormData();

        // Update the formData object
        formData.append(
            "File",
            this.state.selectedFile,
            this.state.selectedFile.name
        );

        // Details of the uploaded file
        console.log(this.state.selectedFile);

        // Request made to the backend api
        // Send formData object
        //axios.post("/api/Vehicle/UploadFile", formData);

        fetch('/api/vehicle/upload',
            {
                method: 'POST',
                body: formData,
            }
        )
        .then((response) => response.json())
        .then((result) => {
                console.log('Success:', result);
        })
        .catch((error) => {
                console.error('Error:', error);
        });
    };

    fileData = () => {

        if (this.state.selectedFile) {

            return (
                <div>
                    <h2>File Details:</h2>
                    <p>File Name: {this.state.selectedFile.name}</p>
                    <p>File Type: {this.state.selectedFile.type}</p>
                </div>
            );
        } else {
            return (
                <div>
                    <br />
                    <h4>Choose a file!</h4>
                </div>
            );
        }
    };

    render() {
        return (
            <div>
                <h1 id="tabelLabel" >upload vehcile file!</h1>
                <p>Upload vehicle sales data file to visualize the data.</p>
                <div>
                    <div>
                        <input type="file" onChange={this.onFileChange} />
                        <button onClick={this.onFileUpload}>
                            Upload!
                        </button>
                    </div>
                    {this.fileData()}
                </div>
            </div>
        );
    }
}
