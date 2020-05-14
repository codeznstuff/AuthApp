import React from 'react';
import { useAbility } from '../../../../../context/CASL/AbilityContext';
import { Button } from 'react-bootstrap';

export const GridActionButtons = (props: any) => {
  const ability = useAbility();

  if (props.actionType === 'delete') {
    return (
      <>
        <Button className="btn ml-0" onClick={() => alert('Delete' + props.data.displayName)} disabled={!ability.can('delete', props.data)}>
          Delete
        </Button>
      </>
    );
  }
};

export default GridActionButtons;
