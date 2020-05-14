import { ColDef, GridReadyEvent, RowDoubleClickedEvent } from 'ag-grid-community';
import 'ag-grid-community/dist/styles/ag-grid.css';
import 'ag-grid-community/dist/styles/ag-theme-balham.css';
import 'ag-grid-enterprise';
import { AgGridReact } from 'ag-grid-react';
import React, { useState } from 'react';
import { useAsync } from 'react-async';
import { Col, Form, Row } from 'react-bootstrap';
import { useAlerts } from '../../../../context/AlertsContext/AlertsContext';
import { fetchUsers, IApplicationUser, IClaimData } from '../../../../services/AuthorizationApi';
import GridActionButtons from './GridActionButtons/GridActionButtons';

export const UserGrid = () => {
  const [rowData, setRowData] = useState();
  const [gridApi, setGridApi] = useState();
  const { createAlert } = useAlerts();

  const { run, isPending } = useAsync({
    deferFn: fetchUsers,
    onReject: () => {
      createAlert({ variant: 'danger', heading: 'Whoops!', text: 'Something went wrong, please try again later' });
    }
  });

  const handleOnGridReady = async (params: GridReadyEvent): Promise<void> => {
    setGridApi(params.api);
    params.api.sizeColumnsToFit();
    const data: Array<IApplicationUser> = await run();
    setRowData(data);
  };

  const handleOnChange = (event: any): void => {
    gridApi.setQuickFilter(event.target.value);
  };

  const handleOnRowDoubleClicked = (event: RowDoubleClickedEvent): void => {
    window.alert('SELECTED: ' + event.node.data.DisplayName);
  };

  const defaultColDef: ColDef = { sortable: true, filter: true, resizable: false, menuTabs: ['filterMenuTab'], suppressMovable: true };

  const columnDefs: Array<ColDef> = [
    {
      cellRenderer: 'actionsRenderer',
      cellRendererParams: {
        actionType: 'delete'
      },
      width: 50,
      menuTabs: []
    },
    {
      headerName: 'ID',
      field: 'userId'
    },
    {
      headerName: 'Name',
      field: 'displayName'
    },
    {
      headerName: 'Email',
      field: 'emailAddress'
    },
    {
      headerName: 'Claims',
      field: 'claims',
      valueFormatter: (params: any) => {
        let result: string;
        params.value.forEach((element: IClaimData) => {
          if (result) {
            result = result + ', ' + element.claimType + ': ' + element.claimValue;
          } else {
            result = element.claimType + ': ' + element.claimValue;
          }
        });
        return result;
      }
    }
  ];

  const rowSelection: string = 'single';

  const frameworkComponents: any = {
    actionsRenderer: GridActionButtons
  };

  return (
    <>
      <Row className="my-4 align-items-end">
        <Col sm={12} md={4} lg={3}>
          <Form.Group className="m-0">
            <Form.Label>Search Users</Form.Label>
            <Form.Control placeholder="Search..." onChange={handleOnChange} />
          </Form.Group>
        </Col>
        <Col>{isPending && <div />}</Col>
      </Row>
      <div style={{ height: '1000px', width: '100%' }} className="ag-theme-balham">
        <AgGridReact
          reactNext
          animateRows
          defaultColDef={defaultColDef}
          columnDefs={columnDefs}
          rowSelection={rowSelection}
          onGridReady={handleOnGridReady}
          onRowDoubleClicked={handleOnRowDoubleClicked}
          context={{ ...rowData }}
          frameworkComponents={frameworkComponents}
          rowData={rowData}
          rowHeight={40}
        />
      </div>
    </>
  );
};

export default UserGrid;
