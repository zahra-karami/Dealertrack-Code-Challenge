import React, { Component } from 'react';
import NumberFormat from 'react-number-format';
import dateFormat from 'dateformat';
import { PlusLg, Trash } from 'react-bootstrap-icons';

export class FetchVehicles extends Component {
    static displayName = FetchVehicles.name;

    constructor(props) {
        super(props);
        this.state = { vehicles: [], mostOftenSoldVehicle: '', loading: true };
    }

    componentDidMount() {
        this.populateVehiclesData();
    }

    async populateVehiclesData() {
        const response = await fetch('/api/vehicle/get');
        const data = await response.json();
        this.setState({ vehicles: data.result.list, mostOftenSoldVehicle: data.result.mostOftenSoldVehicle, loading: false });
    }

    async deleteVehcile(id) {

        await fetch('/api/vehicle/delete/' + id ,
            {
                method: 'POST',
            })
            .then((response) => response.json())
            .then((result) => {
                if (result.isSucceeded === true) {
                    this.populateVehiclesData();
                }
            })
            .catch((error) => {
                console.error('Error:', error);
            });

    }

    rendervehiclesTable = (vehicles) => {
        return (
            <div class="table-responsive">
                <div class="table-wrapper">
                    <div class="table-title">
                        <div class="row">
                            <div class="col-sm-8"><h2>Sale Vehicle List</h2></div>
                            <div class="col-sm-4 right">                               
                                <a class="btn btn-success btn-s" href="/upload-file"><PlusLg /> Upload New File</a>
                            </div>
                        </div>
                        <div class="row">
                            <p>This component demonstrates fetching data from AWS DynamoDb.</p>
                        </div>
                        <div class="row" >
                            <p class="alert alert-primary" role="alert">
                                The most often sold vehicle : <strong>{this.state.mostOftenSoldVehicle}</strong> </p>
                        </div>

                    </div>


                    <table className='table table-striped' aria-labelledby="tabelLabel">
                        <thead>
                            <tr>
                                <th>Deal Number</th>
                                <th>Customer Name</th>
                                <th>Dealership Name</th>
                                <th>Vehicle</th>
                                <th>Price</th>
                                <th>Date</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {vehicles.map(vehicle =>
                                <tr key={vehicle.dealNumber}>

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
                                        <a class="delete" title="delete vehicle" onClick={(e) => this.deleteVehcile(vehicle.id)} ><Trash /></a>
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
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.rendervehiclesTable(this.state.vehicles);

        return (
            <div class="container-lg">
                {contents}
            </div>
        );
    }


}
