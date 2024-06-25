
export enum ObjectIds {
   PlantaEvaporador1 = 'i=252',
   PlantaEvaporador1_Tanque1 = 'i=253',
   PlantaEvaporador1_Tanque1_TT003 = 'i=254',
   PlantaEvaporador1_Tanque2 = 'i=262',
   PlantaEvaporador1_Tanque2_TT001 = 'i=263',
   PlantaEvaporador1_Tanque2_TV001 = 'i=271',
   PlantaEvaporador1_Tanque3 = 'i=284',
   PlantaEvaporador1_Tanque3_PT001 = 'i=285',
   PlantaEvaporador1_Tanque3_LT001 = 'i=293',
   PlantaEvaporador1_T1paraT2 = 'i=301',
   PlantaEvaporador1_T1paraT2_SC001 = 'i=302',
   PlantaEvaporador1_T1paraT3 = 'i=309',
   PlantaEvaporador1_T1paraT3_SC003 = 'i=310',
   PlantaEvaporador1_T2paraCx = 'i=317',
   PlantaEvaporador1_T2paraCx_FV002 = 'i=318',
   PlantaEvaporador1_T3paraCx = 'i=331',
   PlantaEvaporador1_T3paraCx_FV001 = 'i=332',
   PlantaEvaporador1_T3paraCx_SC002 = 'i=345',
   PlantaEvaporador1_T3paraCx_TT002 = 'i=352',
   PlantaEvaporador1_T3paraCx_SC003 = 'i=360'
}

export enum ObjectTypeIds {
   GenericoSensorType = 'i=1',
   GenericoAtuadorType = 'i=9',
   GenericoInvFreqType = 'i=22',
   IndicadorNivelType = 'i=29',
   IndicadorTempType = 'i=30',
   MedidorPressaoType = 'i=31',
   ValvulaType = 'i=32',
   ResistenciaType = 'i=33',
   InvMotorType = 'i=34',
   Tanque1Type = 'i=35',
   Tanque2Type = 'i=44',
   Tanque3Type = 'i=66',
   T1paraT2Type = 'i=83',
   T1paraT3Type = 'i=91',
   T2paraCxType = 'i=99',
   T3paraCxType = 'i=113',
   EvaporadorPlantaType = 'i=142'
}

export enum VariableIds {
   PlantaEvaporador1_Tanque1_TT003_Output = 'i=255',
   PlantaEvaporador1_Tanque1_TT003_Output_EURange = 'i=259',
   PlantaEvaporador1_Tanque1_TT003_Unidade = 'i=261',
   PlantaEvaporador1_Tanque2_TT001_Output = 'i=264',
   PlantaEvaporador1_Tanque2_TT001_Output_EURange = 'i=268',
   PlantaEvaporador1_Tanque2_TT001_Unidade = 'i=270',
   PlantaEvaporador1_Tanque2_TV001_Input = 'i=272',
   PlantaEvaporador1_Tanque2_TV001_Input_EURange = 'i=276',
   PlantaEvaporador1_Tanque2_TV001_Output = 'i=278',
   PlantaEvaporador1_Tanque2_TV001_Output_EURange = 'i=282',
   PlantaEvaporador1_Tanque3_PT001_Output = 'i=286',
   PlantaEvaporador1_Tanque3_PT001_Output_EURange = 'i=290',
   PlantaEvaporador1_Tanque3_PT001_Unidade = 'i=292',
   PlantaEvaporador1_Tanque3_LT001_Output = 'i=294',
   PlantaEvaporador1_Tanque3_LT001_Output_EURange = 'i=298',
   PlantaEvaporador1_Tanque3_LT001_Unidade = 'i=300',
   PlantaEvaporador1_T1paraT2_SC001_pwm = 'i=303',
   PlantaEvaporador1_T1paraT2_SC001_pwm_EURange = 'i=307',
   PlantaEvaporador1_T1paraT3_SC003_pwm = 'i=311',
   PlantaEvaporador1_T1paraT3_SC003_pwm_EURange = 'i=315',
   PlantaEvaporador1_T2paraCx_FV002_Input = 'i=319',
   PlantaEvaporador1_T2paraCx_FV002_Input_EURange = 'i=323',
   PlantaEvaporador1_T2paraCx_FV002_Output = 'i=325',
   PlantaEvaporador1_T2paraCx_FV002_Output_EURange = 'i=329',
   PlantaEvaporador1_T3paraCx_FV001_Input = 'i=333',
   PlantaEvaporador1_T3paraCx_FV001_Input_EURange = 'i=337',
   PlantaEvaporador1_T3paraCx_FV001_Output = 'i=339',
   PlantaEvaporador1_T3paraCx_FV001_Output_EURange = 'i=343',
   PlantaEvaporador1_T3paraCx_SC002_pwm = 'i=346',
   PlantaEvaporador1_T3paraCx_SC002_pwm_EURange = 'i=350',
   PlantaEvaporador1_T3paraCx_TT002_Output = 'i=353',
   PlantaEvaporador1_T3paraCx_TT002_Output_EURange = 'i=357',
   PlantaEvaporador1_T3paraCx_TT002_Unidade = 'i=359',
   PlantaEvaporador1_T3paraCx_SC003_pwm = 'i=361'
}

export class StatusCode {
   public static isGood(code?: number): boolean {
      return !code || (code & 0xD0000000) === 0;
   }
   public static isUncertain(code?: number): boolean {
      return (code ?? 0 & 0x40000000) !== 0;
   }
   public static isBad(code?: number): boolean {
      return (code ?? 0 & 0x80000000) !== 0;
   }
}