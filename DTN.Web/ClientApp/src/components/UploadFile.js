import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import NumberFormat from 'react-number-format';
import dateFormat from 'dateformat';
import { PlusLg, Arrow90degUp, Upload } from 'react-bootstrap-icons';

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

    cleanForm = () => {
        this.setState({ selectedFile: null, uploaded: false, loading: false, vehicles: [], mostOftenSoldVehicle: '' });
    };

    onFileUpload = () => {
        console.log(this.state.selectedFile);

        if (this.state.selectedFile == null) return;
        this.setState({ uploaded: true, loading: true });

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
                    this.setState({ vehicles: result.result.list, loading: false });
                    this.setState({ mostOftenSoldVehicle: result.result.mostOftenSoldVehicle });
                }
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    };

    fileData = () => {

        if (this.state.uploaded === true) {
            let contents = this.state.loading
                ? <p><em>Loading...</em></p>
                : this.rendervehiclesTable(this.state.vehicles);

            return (
                <div class="container-lg">
                    {contents}
                </div>
            );
        } else {
            return (
                <div class="form-group">
                    <h1 id="tabelLabel" >Upload a vehcile file!</h1>
                    <div class="mb-3">
                        <label for="formFile" class="form-label">Upload vehicle sales data file to visualize the data.</label>
                        <input class="form-control" type="file" onChange={this.onFileChange} id="formFile" />
                    </div>
                    <div class="mb-3">
                        <i><small class="form-text text-muted">Maximum allowed file size is 1MB. </small></i>
                        <i><small class="form-text text-muted">Accepted file types : .csv </small></i>

                    </div>
                    <div class="mb-3">
                        <p></p>
                        <button onClick={this.onFileUpload} class="btn btn-primary btn-s" style={{ marginRight: "5px" }}> <Upload /> Upload File</button> 
                        <a class="btn btn-success btn-s" href="/fetch-vehicles"><Arrow90degUp /> Back to the List</a>
                    </div>

                </div >
            );
        }
    };

    rendervehiclesTable = (vehicles) => {
        return (
            <div class="table-responsive">
                <div class="table-wrapper">
                    <div class="table-title">
                        <div class="row">
                            <p class="alert alert-success" role="alert">
                                {this.state.selectedFile.name} successfully uploaded
                            </p>
                        </div>
                        <div class="row" >
                            <p class="alert alert-primary" role="alert">The most often sold vehicle : <strong>{this.state.mostOftenSoldVehicle}</strong> </p>
                        </div>
                        <div class="btn-toolbar">
                            <button onClick={this.cleanForm} class="btn btn-danger" style={{ marginRight: "5px" }}><PlusLg /> Upload Another File</button>
                            <a class="btn btn-success btn-s" href="/fetch-vehicles"><Arrow90degUp /> Back to the List</a>
                        </div>
                    </div>
                    <table className='table' aria-labelledby="tabelLabel">
                        <thead>
                            <tr>
                                <th>Deal Number</th>
                                <th>Customer Name</th>
                                <th>Dealership Name</th>
                                <th>Vehicle</th>
                                <th>Price</th>
                                <th>Date</th>
                                <th>Result</th>
                            </tr>
                        </thead>
                        <tbody>
                            {vehicles.map(vehicle =>
                                <tr key={vehicle.dealNumber} className={vehicle.id == null
                                    ? 'table-danger'
                                    : 'table-success'}>

                                    <td>{vehicle.dealNumber}</td>
                                    <td>{vehicle.customerName}</td>
                                    <td>{vehicle.dealershipName}</td>
                                    <td>{vehicle.vehicle}</td>
                                    <td>
                                        <NumberFormat
                                            value={vehicle.price}
                                            className="foo"
                                            displayType={'text'}
                                            thousandSeparator={true}
                                            prefix={'CAD$'}
                                            renderText={(value, props) => <div {...props}>{value}</div>}
                                        />
                                    </td>
                                    <td>
                                        {dateFormat(vehicle.date, 'yyyy/mm/dd')}
                                    </td>
                                    <td>
                                        {vehicle.id == null
                                            ? 'duplicated record!'
                                            : 'success!'}
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
        );
    }
    render() {
        return (
            <div class="container">
                {this.fileData()}
            </div>
        );
    }
}
