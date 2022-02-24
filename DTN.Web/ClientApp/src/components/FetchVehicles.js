import React, { Component } from 'react';
import NumberFormat from 'react-number-format';
import dateFormat from 'dateformat';

export class FetchVehicles extends Component {
    static displayName = FetchVehicles.name;

    constructor(props) {
        super(props);
        this.state = { vehicles: [], loading: true };
    }

    componentDidMount() {
        this.populateVehiclesData();
    }

    rendervehiclesTable = (vehicles) => {
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
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.rendervehiclesTable(this.state.vehicles);

        return (
            <div>
                <h1 id="tabelLabel" >Weather vehicle</h1>
                <p>This component demonstrates fetching data from AWS DynamoDb.</p>
                {contents}
            </div>
        );
    }

    async populateVehiclesData() {
        const response = await fetch('/api/vehicle/get');
        const data = await response.json();
        this.setState({ vehicles: data.result.list, loading: false });
    }
}
