export interface User {
}

export interface registerData {
  Username: string;
  Mobile: string;
  Password: string
}

export interface workSchedule {
  startTimeMorning: string;
  startTimeEvening: string;
  endTimeMorning: string;
  endTimeEvening: string;
  scopeTime: string;
  saturdayWork: boolean;
  sundayWork: boolean;
  mondayWork: boolean;
  tuesdayWork: boolean;
  wednesdayWork: boolean;
  thursdayWork: boolean;
  fridayWork: boolean
}

export interface clientAppointment {

  appointmentDate: string;
  barberName: string;
  barberShopAddress: string;
  barberShopName: string;
  barberShopPhone: string;
  startTime: string;
  endTime: string;
}
