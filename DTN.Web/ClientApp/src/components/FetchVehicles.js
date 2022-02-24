import React, { Component } from 'react';

export class FetchVehicles extends Component {
    static displayName = FetchVehicles.name;

    constructor(props) {
        super(props);
        this.state = { vehicles: [], loading: true };
    }

    componentDidMount() {
        this.populateVehiclesData();
    }

    static rendervehiclesTable(vehicles) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Deal Number</th>
                        <th>Customer Name</th>
                        <th>Dealership Name</th>
                        <th>Vehicle</th>
                        <th>Price</th>
                        <th>Date</th>
                    </tr>
                </thead>
                <tbody>
                    {vehicles.map(vehicle =>
                        <tr key={vehicle.dealNumber}>
                            <td>{vehicle.customerName}</td>
                            <td>{vehicle.dealershipName}</td>
                            <td>{vehicle.vehicle}</td>
                            <td>{vehicle.price}</td>
                            <td>{vehicle.date}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchVehicles.rendervehiclesTable(this.state.vehicles);

        return (
            <div>
                <h1 id="tabelLabel" >Weather vehicle</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateVehiclesData() {
        const response = await fetch('vehicle');
        const data = await response.json();
        this.setState({ vehicles: data, loading: false });
    }
}
