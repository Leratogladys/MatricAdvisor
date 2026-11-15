# Wits catalogue pilot

Verified on **15 November 2026** for South African NSC applicants. Wits lists the **2027 intake** as closed. No later intake was verified, so the website labels the next intake “Not yet confirmed.”

| Existing programme | Official code | Requirements source |
| --- | --- | --- |
| BSc Computer Science | SBA13 | [Wits Computer Science](https://www.wits.ac.za/course-finder/undergraduate/science/computer-science/) |
| BSc Computational and Applied Mathematics | SBA21 | [Wits Computational and Applied Mathematics](https://www.wits.ac.za/course-finder/undergraduate/science/computational-and-applied-mathematics/) |

Both closed for applications on **30 September 2026** ([Wits applications](https://www.wits.ac.za/undergraduate/apply-to-wits/)). The programme pages list Wits APS 44+, Mathematics Level 6, English HL or FAL Level 5, the required NBTs with a separate 31 October 2026 test deadline, and possible waitlisting at Wits APS 41–43. The [Wits NSC entry guide](https://www.wits.ac.za/undergraduate/entry-requirements/) explains the bachelor’s pass, subject levels and how Wits weights English and Mathematics.

## Updating the pilot

- The migration adds `ProgrammeIntakes`. The existing `Programmes` and `SavedProgrammes` records remain in place.
- Development startup applies migrations and adds the verified intake to each matching programme by university and name. The combination of programme ID and intake year is unique. A restart leaves an existing intake as recorded.
- After verifying a later cycle, add its intake as a new record instead of replacing the historical one. Keep the application deadline empty when it cannot be verified. The browse and detail pages use the most recent recorded intake; programme IDs and saved links stay the same. This seed is a pilot, not a general importer or editor.
- The MVC site lets learners save a pilot programme directly. It shows the official requirements without applying a stored APS of unknown scoring method. Other programmes still use the existing comparison; their catalogue records were not verified in this pilot.
