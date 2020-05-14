import React from 'react';
import { Link } from 'react-router-dom';
import { Navbar, Nav } from 'react-bootstrap';
import { useAuthorization } from '../../../context/Authorization/AuthorizationContext';
import { useAbility } from '../../../context/CASL/AbilityContext';
import PageAlerts from '../PageAlerts/PageAlerts';

export const Header = () => {
  const user = useAuthorization();
  const ability = useAbility();
  const isAdmin = ability.can('manage', 'all');

  return (
    <>
      <Navbar bg="light" variant="light" expand="sm">
        <Link className="navbar-brand" to="/">
          Authorization Demo
        </Link>
        <Navbar.Toggle aria-controls="navbar-nav" />
        <Navbar.Collapse id="navbar-nav">
          <Nav className="mr-auto">
            <Link className="nav-link" to="/">
              All Users
            </Link>
            {isAdmin && <Nav.Link href="/">Admin Only</Nav.Link>}
          </Nav>
          <Navbar.Text>
            {user.firstName} {user.lastName}
          </Navbar.Text>
        </Navbar.Collapse>
      </Navbar>
      <PageAlerts />
    </>
  );
};
