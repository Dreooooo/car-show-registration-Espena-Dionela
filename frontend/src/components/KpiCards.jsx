import KpiCard from "./KpiCard";

export default function KpiCards({ kpis }) {
  const fmt = (val) =>
    '₱' + Number(val ?? 0).toLocaleString('en-PH');

  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-5 gap-4">
      <KpiCard
        title="Total Registrations"
        value={kpis.totalRegistrations ?? 0}
        accentColor="border-l-blue-600"
      />

      <KpiCard
        title="Total Payments"
        value={fmt(kpis.totalPayments)}
        accentColor="border-l-emerald-600"
      />

      <KpiCard
        title="Uncollected Payments"
        value={fmt(kpis.uncollectedPayments)}
        accentColor="border-l-amber-500"
      />

      <KpiCard
        title="Confirmed"
        value={kpis.confirmed ?? 0}
        accentColor="border-l-indigo-600"
      />

      <KpiCard
        title="Checked In"
        value={kpis.checkedIn ?? 0}
        accentColor="border-l-purple-600"
      />
    </div>
  );
}

