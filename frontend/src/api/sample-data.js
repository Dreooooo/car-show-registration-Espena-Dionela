const participants = [
  ["Juan Dela Cruz", "09171234567", "juan@example.com", "Sorsogon City", "Toyota Supra MK4", "ABC 1234", "Competitor", "Confirmed", 2500],
  ["Maria Santos", "09181234567", "maria@example.com", "Bulan, Sorsogon", "Honda Civic Type R", "DEF 2345", "Exhibitor", "Confirmed", 1500],
  ["Pedro Reyes", "09191234567", "pedro@example.com", "Gubat, Sorsogon", "Mitsubishi Lancer Evo", "GHI 3456", "VIP", "CheckedIn", 5000],
  ["Ana Garcia", "09201234567", "ana@example.com", "Sorsogon City", "Mazda RX-7", "JKL 4567", "Competitor", "Pending", 1000],
  ["Carlos Mendoza", "09211234567", "carlos@example.com", "Matnog, Sorsogon", "Nissan Skyline R34", "MNO 5678", "Exhibitor", "Confirmed", 1500],
  ["Sofia Ramos", "09221234567", "sofia@example.com", "Irosin, Sorsogon", "Subaru WRX STI", "PQR 6789", "VIP", "Confirmed", 5000],
  ["Miguel Torres", "09231234567", "miguel@example.com", "Barcelona, Sorsogon", "Toyota 86", "STU 7890", "Competitor", "Cancelled", 0],
  ["Isabella Flores", "09241234567", "isabella@example.com", "Sorsogon City", "Honda Civic EG", "VWX 8901", "Exhibitor", "CheckedIn", 1500],
  ["Gabriel Cruz", "09251234567", "gabriel@example.com", "Pilar, Sorsogon", "Ford Mustang GT", "YZA 9012", "VIP", "Pending", 2500],
  ["Camila Lopez", "09261234567", "camila@example.com", "Donsol, Sorsogon", "Mazda MX-5", "BCD 0123", "Competitor", "Confirmed", 2500],
  ["Daniel Bautista", "09271234567", "daniel@example.com", "Sorsogon City", "Nissan 350Z", "EFG 1234", "Exhibitor", "Pending", 500],
  ["Andrea Villanueva", "09281234567", "andrea@example.com", "Castilla, Sorsogon", "Toyota GR86", "HIJ 2345", "VIP", "CheckedIn", 5000],
  ["Nathan Castillo", "09291234567", "nathan@example.com", "Juban, Sorsogon", "Honda Integra Type R", "KLM 3456", "Competitor", "Confirmed", 2500],
  ["Patricia Aquino", "09301234567", "patricia@example.com", "Sorsogon City", "Mitsubishi Eclipse", "NOP 4567", "Exhibitor", "Cancelled", 0],
  ["Joshua Fernandez", "09311234567", "joshua@example.com", "Magallanes, Sorsogon", "BMW M3", "QRS 5678", "VIP", "Confirmed", 5000],
  ["Nicole Herrera", "09321234567", "nicole@example.com", "Bacon, Sorsogon", "Toyota Celica", "TUV 6789", "Competitor", "Pending", 1000],
  ["Adrian Morales", "09331234567", "adrian@example.com", "Sorsogon City", "Nissan Silvia S15", "WXY 7890", "Exhibitor", "CheckedIn", 1500],
  ["Beatrice Lim", "09341234567", "beatrice@example.com", "Casiguran, Sorsogon", "Porsche Cayman", "ZAB 8901", "VIP", "Confirmed", 5000],
  ["Christian Navarro", "09351234567", "christian@example.com", "Irosin, Sorsogon", "Subaru BRZ", "CDE 9012", "Competitor", "Cancelled", 0],
  ["Diana Salazar", "09361234567", "diana@example.com", "Sorsogon City", "Honda S2000", "FGH 0123", "Exhibitor", "Confirmed", 1500],
  ["Ethan Rivera", "09371234567", "ethan@example.com", "Gubat, Sorsogon", "Chevrolet Camaro", "IJK 1234", "VIP", "Pending", 3000],
  ["Faith Domingo", "09381234567", "faith@example.com", "Pilar, Sorsogon", "Toyota MR2", "LMN 2345", "Competitor", "CheckedIn", 2500],
  ["Gabrielle Tan", "09391234567", "gabrielle@example.com", "Sorsogon City", "Mazda RX-8", "OPQ 3456", "Exhibitor", "Confirmed", 1500],
  ["Henry Valdez", "09401234567", "henry@example.com", "Matnog, Sorsogon", "Ford Focus RS", "RST 4567", "VIP", "Cancelled", 0],
  ["Irene Cabrera", "09411234567", "irene@example.com", "Barcelona, Sorsogon", "Nissan GT-R", "UVW 5678", "Competitor", "Confirmed", 2500],
];

const fees = {
  Exhibitor: 1500,
  Competitor: 2500,
  VIP: 5000,
};

const registrations = participants.map((person, index) => {
  const [
    participantName,
    contactNumber,
    email,
    address,
    carMakeModel,
    plateNumber,
    registrationType,
    status,
    amountPaid,
  ] = person;

  const registrationFee = fees[registrationType];

  const paymentStatus =
    amountPaid === 0
      ? "Unpaid"
      : amountPaid >= registrationFee
        ? "Paid"
        : "Partial";

  return {
    id: `sample-${index + 1}`,
    registrationNumber: `CS27-${String(index + 1).padStart(4, "0")}`,
    registrationDate: new Date(
      Date.UTC(2027, 1, 10 + index, 8, 30)
    ).toISOString(),
    participantName,
    contactNumber,
    email,
    address,
    carMakeModel,
    plateNumber,
    registrationType,
    status,
    registrationFee,
    amountPaid,
    paymentStatus,
  };
});

export async function getRegistrations({
  participantName = "",
  search = "",
  type = "All",
  status = "All",
  page = 1,
  pageSize = 5,
} = {}) {
  const searchTerm = (participantName || search).toLowerCase().trim();

  const filtered = registrations.filter((registration) => {
    const matchesName = registration.participantName
      .toLowerCase()
      .includes(searchTerm);

    const matchesType =
      type === "All" || registration.registrationType === type;

    const matchesStatus =
      status === "All" || registration.status === status;

    return matchesName && matchesType && matchesStatus;
  });

  const totalCount = filtered.length;
  const totalPages = Math.ceil(totalCount / pageSize);
  const startIndex = (page - 1) * pageSize;

  return {
    items: filtered.slice(startIndex, startIndex + pageSize),
    page,
    pageSize,
    totalCount,
    totalPages,
  };
}

export async function getSummary() {
  const totalRegistrations = registrations.length;

  const totalFees = registrations.reduce(
    (total, item) => total + item.registrationFee,
    0
  );

  const totalPayments = registrations.reduce(
    (total, item) => total + item.amountPaid,
    0
  );

  return {
    totalRegistrations,
    confirmed: registrations.filter((item) => item.status === "Confirmed").length,
    checkedIn: registrations.filter((item) => item.status === "CheckedIn").length,
    pending: registrations.filter((item) => item.status === "Pending").length,
    cancelled: registrations.filter((item) => item.status === "Cancelled").length,
    totalFees,
    totalPayments,
    uncollectedPayments: totalFees - totalPayments,
  };
}