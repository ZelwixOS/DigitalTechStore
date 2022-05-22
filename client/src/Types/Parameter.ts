import ParameterValue from './ParameterValue';

type Parameter = {
  id: string;
  parameterId?: string;
  parameterValueId?: string;
  name: string;
  value: string;
  important: boolean;
  range: boolean;
  minValue: number;
  maxValue: number;
  parameterBlockId: string;
  parameterValues: ParameterValue[];
};

export default Parameter;
